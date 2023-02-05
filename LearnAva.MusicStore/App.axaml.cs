using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LearnAva.MusicStore.ViewModels;
using LearnAva.MusicStore.Views;
using System.Diagnostics;
using Splat;

namespace LearnAva.MusicStore;

public partial class App : Application, IEnableLogger
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        this.Log().Debug("Hello World!");
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public void RegisterAppServices()
    {
        // I only want to hear about errors
        var logger = new DebugLogger() { Level = LogLevel.Debug };
        Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));
    }
}