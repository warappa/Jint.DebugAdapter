using Jither.DebugAdapter.Protocol.Types;

namespace Jither.DebugAdapter.Protocol.Responses;

public class ErrorResponse : ProtocolResponseBody
{
    private int NextId => field++;

    public Message Error { get; set; }

    public ErrorResponse(Exception ex)
    {
        Error = new Message(NextId, ex.Message);
    }
}
