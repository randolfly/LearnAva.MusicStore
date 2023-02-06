using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LearnAva.MusicStore.Library.ViewModels;
using LearnAva.MusicStore.Views;
using ReactiveUI;
using Splat;

namespace LearnAva.MusicStore;

public class App : Application, IEnableLogger
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
            var mainWindow = Locator.Current.GetService<IViewFor<MainWindowViewModel>>() as MainWindow
                             ?? throw new Exception("Exception while create MusicStoreWindow");
            mainWindow.DataContext = new MainWindowViewModel();
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}