using Jint.Runtime.Debugger;

namespace Jint.DebugAdapter;

public static class JintOptionsExtensions
{
    extension(Options options)
    {
        public Options SetupDebugger()
        {
            return options
                // In order to stop on entry, we need to be stepping from the start
                // The Debugger will change to StepMode.None if not stopping on entry.
                .InitialStepMode(StepMode.Into)
                .DebuggerStatementHandling(DebuggerStatementHandling.Script);
        }
    }
}
