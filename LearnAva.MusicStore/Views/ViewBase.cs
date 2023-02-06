using Avalonia.Controls;
using Avalonia.Controls.Templates;
using LearnAva.MusicStore.Library.ViewModels;
using ReactiveUI;
using Splat;

namespace LearnAva.MusicStore.Views;

public abstract class ViewBase<TViewModel> : UserControl, IDataTemplate, IViewFor<TViewModel> where TViewModel : ViewModelBase
{
    public IControl? Build(object? param)
    {
        return Locator.Current.GetService(typeof(IViewFor<TViewModel>)) as IControl;
    }

    public bool Match(object? data)
    {
        return data is TViewModel;
    }

    public TViewModel? ViewModel { get; set; }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (TViewModel?)value;
    }
}