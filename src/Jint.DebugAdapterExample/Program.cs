using Jint.DebugAdapter;
using Jint.DebugAdapterExample;
using Jither.DebugAdapter;
using Jither.DebugAdapter.Helpers;

var demosById = new Dictionary<string, Action<AppOptions>>
{
    ["files"] = DemoFiles,
    ["internal"] = DemoInternal,
    ["running"] = DemoRunning
};

LogManager.Level = LogLevel.Verbose;
LogManager.Provider = new ConsoleLogProvider();

var options = new AppOptions(args, demosById);
var demo = demosById[options.DemoId];

demo(options);

return 0;

static Endpoint CreateEndpoint(string? endpoint)
{
    if (endpoint is null)
    {
        return new StdInOutEndpoint();
    }
    if (int.TryParse(endpoint, out var port))
    {
        return new TcpEndpoint(port);
    }

    return new NamedPipeEndpoint(endpoint);
}

static void DemoFiles(AppOptions appOptions)
{
    var endpoint = CreateEndpoint(appOptions.Endpoint);

    var host = new FilesScriptHost();
    var adapter = new JintAdapter(host, host.Engine, endpoint);
    host.RegisterConsole(adapter.Console);

    adapter.StartListening();
}

static void DemoInternal(AppOptions appOptions)
{
    var endpoint = CreateEndpoint(appOptions.Endpoint);

    var host = new InternalScriptHost();
    var adapter = new JintAdapter(host, host.Engine, endpoint);
    host.RegisterConsole(adapter.Console);

    adapter.StartListening();
}

static void DemoRunning(AppOptions appOptions)
{
    var endpoint = CreateEndpoint(appOptions.Endpoint);

    var host = new RunningScriptHost();
    var adapter = new JintAdapter(host, host.Engine, endpoint);
    host.RegisterConsole(adapter.Console);

    adapter.Launch("scripts/index.js");
}
