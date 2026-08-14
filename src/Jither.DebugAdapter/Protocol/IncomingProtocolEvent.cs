using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Events;

namespace Jither.DebugAdapter.Protocol;

public class IncomingProtocolEvent<TBody> : BaseProtocolEvent
    where TBody : ProtocolEventBody
{
    public IncomingProtocolEvent(string @event)
        :base(@event)
    {
    }
    
    [JsonPropertyOrder(100)]
    public required TBody Body { get; set; }

    [JsonIgnore]
    public override ProtocolEventBody UntypedBody => Body;
}
