using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime.Descriptors;

namespace Jint.DebugAdapter.Variables;

public class PropertyVariableContainer : VariableContainer
{
    private readonly PropertyDescriptor property;
    private readonly ObjectInstance owner;

    public PropertyVariableContainer(VariableStore store, int id, PropertyDescriptor property, ObjectInstance owner)
        : base(store, id)
    {
        this.property = property;
        this.owner = owner;
    }

    public override JsValue SetVariable(string name, JsValue value)
    {
        // TODO: Is this right?
        throw new VariableException($"Cannot modify property value {name}");
    }

    protected override IEnumerable<JintVariable> GetAllVariables(int? start, int? count)
    {
        return [CreateVariable(string.Empty, owner.Engine.Invoke(property.Get!, owner, []))];
    }
}
