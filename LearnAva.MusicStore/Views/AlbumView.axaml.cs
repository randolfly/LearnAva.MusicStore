using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using LearnAva.MusicStore.Library.ViewModels;
using ReactiveUI;

namespace LearnAva.MusicStore.Views;

public partial class AlbumView : UserControl, IViewFor<AlbumViewModel>, IDataTemplate
{
    public AlbumView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (AlbumViewModel?)value;
    }

    public AlbumViewModel? ViewModel { get; set; }
    public IControl? Build(object? param)
    {
        throw new System.NotImplementedException();
    }

    public bool Match(object? data)
    {
        throw new System.NotImplementedException();
    }
}