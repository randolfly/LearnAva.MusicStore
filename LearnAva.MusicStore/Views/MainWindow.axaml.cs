using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.ReactiveUI;
using LearnAva.MusicStore.Library.ViewModels;
using ReactiveUI;
using Splat;

namespace LearnAva.MusicStore.Views;

[SingleInstanceView]
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>, IEnableLogger
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.WhenActivated(d => d(ViewModel.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<MusicStoreViewModel, AlbumViewModel?> interaction)
    {
        this.Log().Info("Show MusicStoreWindow");
        var dialog = Locator.Current.GetService<IViewFor<MusicStoreViewModel>>() as MusicStoreWindow
                     ?? throw new Exception("Exception while create MusicStoreWindow");
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<AlbumViewModel?>(this);
        interaction.SetOutput(result);
    }
}