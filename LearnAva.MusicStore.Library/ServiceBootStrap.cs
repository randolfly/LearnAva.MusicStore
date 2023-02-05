using Avalonia;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Services;
using LearnAva.MusicStore.Library.ViewModels;
using Splat;

namespace LearnAva.MusicStore.Library;

public static class ServiceBootStrap
{
    /// <summary>
    ///     register the services in library
    /// </summary>
    public static void RegisterAppServices()
    {
        // config log service
        var logger = new DebugLogger { Level = LogLevel.Info };
        Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));

        // config interface-service
        Locator.CurrentMutable.RegisterLazySingleton<IAlbumService>(() => new AlbumService());
    }

    /// <summary>
    ///     register service in library with fluent paradigm
    /// </summary>
    /// <param name="appBuilder">AppBuilder in Program.cs</param>
    /// <returns></returns>
    public static AppBuilder RegisterAppServices(this AppBuilder appBuilder)
    {
        RegisterAppServices();
        return appBuilder;
    }
}