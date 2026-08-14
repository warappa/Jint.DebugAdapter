using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime;

namespace Jint.DebugAdapter.Helpers;

/// <summary>
/// Extensions for Jint types.
/// </summary>
public static class JintExtensions
{
    private static readonly JsString lengthPropertyName = new("length");

    extension(ObjectInstance obj)
    {
        /// <summary>
        /// Returns "length" property of an object instance as an integer (assuming it's an array-like). 0 if property doesn't exist or not a valid length.
        /// </summary>
        public uint GetLengthValue()
        {
            return (uint)TypeConverter.ToLength(obj.Get(lengthPropertyName));
        }
    }
}
