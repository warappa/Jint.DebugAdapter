using Jint.DebugAdapter;
using Jint.DebugAdapterExample;
using Jither.DebugAdapter;
using Jither.DebugAdapter.Helpers;

Dictionary<string, Action<Options>> DemosById = new()
{
    ["files"] = DemoFiles,
    ["internal"] = DemoInternal,
    ["running"] = DemoRunning
};

LogManager.Level = LogLevel.Verbose;
LogManager.Provider = new ConsoleLogProvider();

var options = new Options(args, DemosById);
var demo = DemosById[options.DemoId];

demo(options);

return 0;

static Endpoint CreateEndpoint(string? endpoint)
{
    if (endpoint == null)
    {
        return new StdInOutEndpoint();
    }
    if (Int32.TryParse(endpoint, out int port))
    {
        return new TcpEndpoint(port);
    }
    return new NamedPipeEndpoint(endpoint);
}

static void DemoFiles(Options options)
{
    var endpoint = CreateEndpoint(options.Endpoint);

    var host = new FilesScriptHost();
    var adapter = new JintAdapter(host, host.Engine, endpoint);
    host.RegisterConsole(adapter.Console);

    adapter.StartListening();
}

static void DemoInternal(Options options)
{
    var endpoint = CreateEndpoint(options.Endpoint);

    var host = new InternalScriptHost();
    var adapter = new JintAdapter(host, host.Engine, endpoint);
    host.RegisterConsole(adapter.Console);

    adapter.StartListening();
}

static void DemoRunning(Options options)
{
    var endpoint = CreateEndpoint(options.Endpoint);

    var host = new RunningScriptHost();
    var adapter = new JintAdapter(host, host.Engine, endpoint);
    host.RegisterConsole(adapter.Console);

    adapter.Launch("scripts/index.js");
}

public class Options
{
    public string DemoId { get; } = "files";
    public string? Endpoint { get; }

    public Options(string[] args, Dictionary<string, Action<Options>> DemosById)
    {
        foreach (var arg in args)
        {
            if (DemosById.ContainsKey(arg))
            {
                DemoId = arg;
                continue;
            }
            Endpoint = arg;
        }
    }
}
