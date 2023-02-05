using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Services;
using LearnAva.MusicStore.Library.ViewModels;
using LearnAva.MusicStore.Views;
using Splat;

namespace LearnAva.MusicStore;

public class App : Application, IEnableLogger
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        RegisterAppServices();

        this.Log().Debug("Hello World!");
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };

        base.OnFrameworkInitializationCompleted();
    }

    public void RegisterAppServices()
    {
        // config log service
        var logger = new DebugLogger { Level = LogLevel.Debug };
        Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));

        // normal service
        Locator.CurrentMutable.RegisterConstant<IAlbumService>(new AlbumService());
    }
}