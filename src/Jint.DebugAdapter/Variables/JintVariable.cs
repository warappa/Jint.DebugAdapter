using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Types;

namespace Jint.DebugAdapter.Variables;

public class JintVariable : Variable
{
    public JintVariable(string name, string value)
        : base(name, value)
    {
    }

    [JsonIgnore]
    public int SortOrder { get; set; }
}
