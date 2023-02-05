using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LearnAva.MusicStore.Library.ViewModels;
using ReactiveUI;

namespace LearnAva.MusicStore.Views;

public partial class AlbumView : UserControl
{
    public AlbumView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}