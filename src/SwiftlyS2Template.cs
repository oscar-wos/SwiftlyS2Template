using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2Template.Contracts;
using SwiftlyS2Template.Services;

namespace SwiftlyS2Template;

[PluginMetadata(Id = "SwiftlyS2Template", Version = "0.0.0", Name = "SwiftlyS2Template")]
public partial class SwiftlyS2Template(ISwiftlyCore core) : BasePlugin(core)
{
    private IServiceProvider? _serviceProvider;
    private ILogService? _logService;

    public override void ConfigureSharedInterface(IInterfaceManager interfaceManager)
    {
        ServiceCollection services = new();

        _ = services.AddSwiftly(Core);
        _ = services.AddSingleton<ILogService, LogService>();

        _serviceProvider = services.BuildServiceProvider();

        _logService = _serviceProvider.GetRequiredService<ILogService>();
        _logService.LogInformation("SwiftlyS2Template Configured", logger: Core.Logger);
    }

    public override void Load(bool hotReload) { }

    public override void Unload() => (_serviceProvider as IDisposable)?.Dispose();
}
