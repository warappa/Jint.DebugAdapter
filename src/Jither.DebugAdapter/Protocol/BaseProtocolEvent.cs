using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Events;

namespace Jither.DebugAdapter.Protocol;

public abstract class BaseProtocolEvent : ProtocolMessage
{
    public const string TypeName = "event";

    protected BaseProtocolEvent(string @event)
        : base(TypeName)
    {
        Event = @event;
    }

    [JsonPropertyOrder(-10)]
    public string Event { get; set; }

    public abstract ProtocolEventBody UntypedBody { get; }
}
