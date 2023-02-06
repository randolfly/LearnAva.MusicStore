using System.Reflection;
using Avalonia;
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
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetAssembly(typeof(App)));
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