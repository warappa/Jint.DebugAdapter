using System.Text.Json.Serialization;
using Jither.DebugAdapter.Protocol.Responses;

namespace Jither.DebugAdapter.Protocol;

public abstract class BaseProtocolResponse : ProtocolMessage
{
    public const string TypeName = "response";

    protected BaseProtocolResponse(string command)
        : base(TypeName)
    {
        Command = command;
    }

    [JsonPropertyOrder(-10)]
    public string Command { get; set; }

    [JsonPropertyOrder(-9)]
    public bool Success { get; set; }

    [JsonPropertyName("request_seq")]
    public int RequestSeq { get; set; }

    public string? Message { get; set; }

    public abstract ProtocolResponseBody? UntypedBody { get; }
}
