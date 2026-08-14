using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Requests;

namespace Jither.DebugAdapter.Protocol;

public class IncomingProtocolRequest<TArguments> : IncomingProtocolRequest
    where TArguments : ProtocolArguments
{
    [JsonPropertyOrder(100)]
    public TArguments Arguments { get; set; }

    [JsonIgnore]
    public override ProtocolArguments UntypedArguments => Arguments;

    internal override void Sanitize(ProtocolArguments arguments)
    {
        Arguments = arguments as TArguments;
    }
}

public abstract class IncomingProtocolRequest : BaseProtocolRequest
{
    internal abstract void Sanitize(ProtocolArguments arguments);
}
