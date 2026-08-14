using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Events;

namespace Jither.DebugAdapter.Protocol;

public class ProtocolEvent : BaseProtocolEvent
{
    [JsonIgnore]
    public ProtocolEventBody Body { get; private set; }

    [JsonIgnore]
    public override ProtocolEventBody UntypedBody => Body;

    [JsonPropertyName("body"), JsonPropertyOrder(100)]
    public object SerializedBody => Body;

    public ProtocolEvent(string evt, ProtocolEventBody body)
    {
        Event = evt;
        Body = body;
    }
}
