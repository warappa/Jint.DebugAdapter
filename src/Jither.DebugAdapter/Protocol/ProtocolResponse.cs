using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Responses;

namespace Jither.DebugAdapter.Protocol;

public class ProtocolResponse : BaseProtocolResponse
{
    [JsonIgnore]
    public ProtocolResponseBody Body { get; private set; }

    [JsonIgnore]
    public override ProtocolResponseBody UntypedBody => Body;

    [JsonPropertyName("body"), JsonPropertyOrder(100)]
    public object SerializedBody => Body;

    public ProtocolResponse(string command, int requestSeq, bool success, Responses.ProtocolResponseBody body, string message = null)
    {
        Command = command;
        RequestSeq = requestSeq;
        Success = success;
        Body = body;
        Message = message;
    }
}
