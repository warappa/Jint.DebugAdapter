using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Responses;

namespace Jither.DebugAdapter.Protocol;

public class IncomingProtocolResponse<T> : BaseProtocolResponse
    where T : Responses.ProtocolResponseBody
{
    public IncomingProtocolResponse(string command)
        : base(command)
    {
    }

    [JsonPropertyOrder(100)]
    public T Body { get; set; }

    [JsonIgnore]
    public override ProtocolResponseBody UntypedBody => Body;
}
