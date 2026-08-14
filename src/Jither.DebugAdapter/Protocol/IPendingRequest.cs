namespace Jither.DebugAdapter.Protocol;

internal interface IPendingRequest
{
    bool Cancelled { get; }
    void Cancel();
    ProtocolRequest Request { get; }
}
