using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Requests;

namespace Jither.DebugAdapter.Protocol;

public class ProtocolRequest : BaseProtocolRequest
{
    [JsonIgnore]
    public ProtocolArguments Arguments { get; private set; }

    [JsonIgnore]
    public override ProtocolArguments UntypedArguments => Arguments;

    [JsonPropertyName("arguments"), JsonPropertyOrder(100)]
    public object SerializedArguments => Arguments;

    public ProtocolRequest(string command, ProtocolArguments arguments)
    {
        Command = command;
        Arguments = arguments;
    }
}
