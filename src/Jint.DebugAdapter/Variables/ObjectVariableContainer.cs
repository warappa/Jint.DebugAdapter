using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime;
using Jint.Runtime.Descriptors;

namespace Jint.DebugAdapter.Variables;

public class ObjectVariableContainer : VariableContainer
{
    protected readonly ObjectInstance instance;

    public ObjectVariableContainer(VariableStore store, int id, ObjectInstance instance)
        : base(store, id)
    {
        this.instance = instance;
    }

    public override JsValue SetVariable(string name, JsValue value)
    {
        var prop = instance.GetOwnProperty(name);
        if (prop.Writable)
        {
            prop.Value = value;
            return value;
        }

        if (prop.Set is not null)
        {
            instance.Engine.Invoke(prop.Set, value);

            return prop.Get is not null ?
                instance.Engine.Invoke(prop.Get) :
                value;
        }

        throw new VariableException($"Property is read only.");
    }

    protected override IEnumerable<JintVariable> GetNamedVariables(int? start, int? count)
    {
        var props = instance.GetOwnProperties()
            .Concat(GetPrototypeProperties());

        // Return subset/paging
        // TODO: Does this ever happen for anything except arrays in our implementation?
        if (count > 0)
        {
            props = props
                .Skip(start ?? 0)
                .Take(count.Value);
        }

        return AddPrototypeIfExists(
            props
                .Select(p => CreateVariable(p.Key.ToString(), p.Value, instance)));
    }

    protected override IEnumerable<JintVariable> GetAllVariables(int? start, int? count)
    {
        // We don't distinguish between named and indexed variables, except for Array-likes.
        // All properties are returned from GetNamedVariables.
        return GetNamedVariables(start, count);
    }

    protected IEnumerable<JintVariable> AddPrototypeIfExists(IEnumerable<JintVariable> vars)
    {
        if (instance.Prototype is not null)
        {
            var prototype = CreateVariable("[[Prototype]]", instance.Prototype);
            // For prototypes, we want the value to display the prototype's constructor ("type") (a la Chromium devtools)
            prototype.Value = prototype.Type;
            // Place last
            prototype.SortOrder = 10000;
            vars = vars.Append(prototype);
        }

        return vars;
    }

    protected IEnumerable<KeyValuePair<JsValue, PropertyDescriptor>> GetPrototypeProperties()
    {
        // TODO: Handle shadowed prototype properties
        var proto = instance.Prototype;
        while (proto is not null && proto is not ObjectConstructor)
        {
            var props = proto.GetOwnProperties();
            foreach (var prop in props)
            {
                if (prop.Value.Get is not null)
                {
                    yield return prop;
                }
            }
            proto = proto.Prototype;
        }
    }
}
