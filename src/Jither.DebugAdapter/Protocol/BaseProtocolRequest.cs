using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Requests;

namespace Jither.DebugAdapter.Protocol;

public abstract class BaseProtocolRequest : ProtocolMessage
{
    public const string TypeName = "request";

    public BaseProtocolRequest()
    {
        Type = TypeName;
    }

    [JsonPropertyOrder(-10)]
    public string Command { get; set; }

    public abstract ProtocolArguments UntypedArguments { get; }
}
