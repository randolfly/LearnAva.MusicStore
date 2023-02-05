using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Models;
using LearnAva.MusicStore.Library.Services;
using ReactiveUI;
using Splat;

namespace LearnAva.MusicStore.Library.ViewModels;

public class MusicStoreViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isBusy;
    private string? _searchText;
    private AlbumViewModel? _selectedAlbum;

    public MusicStoreViewModel()
    {
        this.WhenAnyValue(x => x.SearchText)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearch!);

        BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (SelectedAlbum is { })
            {
                await SelectedAlbum.SaveToDiskAsync();
                return SelectedAlbum;
            }

            return null;
        });
    }

    public string? SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }

    public ReactiveCommand<Unit, AlbumViewModel?> BuyMusicCommand { get; }

    public ObservableCollection<AlbumViewModel> SearchResults { get; } = new();

    public AlbumViewModel? SelectedAlbum
    {
        get => _selectedAlbum;
        set => this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
    }

    private async void DoSearch(string s)
    {
        IsBusy = true;
        SearchResults.Clear();

        _cancellationTokenSource?.Cancel();

        _cancellationTokenSource = new CancellationTokenSource();

        var albums = await IAlbumService.SearchAsync(s);

        foreach (var album in albums)
        {
            // TODO 修改依赖注入方式，避免一直新建Service
            var vm = new AlbumViewModel(album, Locator.Current.GetService<IAlbumService>());

            SearchResults.Add(vm);
        }

        if (!_cancellationTokenSource.IsCancellationRequested) LoadCovers(_cancellationTokenSource.Token);

        IsBusy = false;
    }

    private async void LoadCovers(CancellationToken cancellationToken)
    {
        foreach (var album in SearchResults.ToList())
        {
            await album.LoadCover();

            if (cancellationToken.IsCancellationRequested) return;
        }
    }
}