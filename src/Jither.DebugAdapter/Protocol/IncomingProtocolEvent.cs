using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Events;

namespace Jither.DebugAdapter.Protocol;

public class IncomingProtocolEvent<TBody> : BaseProtocolEvent
    where TBody : ProtocolEventBody
{
    [JsonPropertyOrder(100)]
    public TBody Body { get; set; }

    [JsonIgnore]
    public override ProtocolEventBody UntypedBody => Body;
}
