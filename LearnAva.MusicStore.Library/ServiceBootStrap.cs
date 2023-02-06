using System.Reflection;
using Avalonia;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Services;
using ReactiveUI;
using Splat;

namespace LearnAva.MusicStore.Library;

public static partial class ServiceBootStrap
{
    /// <summary>
    ///     register the services in library
    /// </summary>
    public static void RegisterLibraryServices()
    {
        // config log service
        var logger = new DebugLogger { Level = LogLevel.Info };
        Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));

        Locator.CurrentMutable.RegisterLazySingleton<IAlbumService>(() => new AlbumService());
    }

    /// <summary>
    ///     register service in library with fluent paradigm
    /// </summary>
    /// <param name="appBuilder">AppBuilder in Program.cs</param>
    /// <returns></returns>
    public static AppBuilder RegisterLibraryServices(this AppBuilder appBuilder)
    {
        RegisterLibraryServices();
        return appBuilder;
    }
}