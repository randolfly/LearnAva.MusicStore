using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using LearnAva.MusicStore.Library.ViewModels;
using ReactiveUI;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace LearnAva.MusicStore.Views;

//[SingleInstanceView]
//可以从建立View的地方找到这个View的作用
//Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetAssembly(typeof(App))!);
public partial class AlbumView : ViewBase<AlbumViewModel>
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