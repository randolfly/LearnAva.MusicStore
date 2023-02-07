using System.Reflection;
using Avalonia;
using LearnAva.MusicStore.Views;
using ReactiveUI;
using Splat;

namespace LearnAva.MusicStore;

public static class ServiceBootStrap
{
    /// <summary>
    ///     register the app views
    /// </summary>
    public static void RegisterAppViews()
    {
        // IViewFor window register
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetAssembly(typeof(App)));
        Locator.CurrentMutable.Register<AlbumView>(() => new AlbumView());
        Locator.CurrentMutable.RegisterLazySingleton<MusicStoreView>(() => new MusicStoreView());
    }

    /// <summary>
    ///     register the views in app with fluent paradigm
    /// </summary>
    /// <param name="appBuilder">AppBuilder in Program.cs</param>
    /// <returns></returns>
    public static AppBuilder RegisterAppViews(this AppBuilder appBuilder)
    {
        RegisterAppViews();
        return appBuilder;
    }
}