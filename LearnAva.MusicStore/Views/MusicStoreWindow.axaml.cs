using System;
using Avalonia;
using Avalonia.ReactiveUI;
using LearnAva.MusicStore.Library.ViewModels;
using ReactiveUI;

namespace LearnAva.MusicStore.Views;

[SingleInstanceView]
public partial class MusicStoreWindow : ReactiveWindow<MusicStoreViewModel>
{
    public MusicStoreWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.WhenActivated(d => d(ViewModel.BuyMusicCommand.Subscribe(Close)));
    }
}