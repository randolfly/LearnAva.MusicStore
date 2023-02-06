using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace LearnAva.MusicStore.Library.ViewModels;

public class MusicStoreViewModel : ViewModelBase
{
    private readonly IAlbumService _albumService;
    private CancellationTokenSource? _cancellationTokenSource;

    public MusicStoreViewModel(IAlbumService? albumService = null)
    {
        _albumService = albumService ?? Locator.Current.GetService<IAlbumService>() ?? new AlbumService();
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

    public ReactiveCommand<Unit, AlbumViewModel?> BuyMusicCommand { get; }

    public ObservableCollection<AlbumViewModel> SearchResults { get; } = new();

    [Reactive] public string? SearchText { get; set; }

    [Reactive] public bool IsBusy { get; set; }

    [Reactive] public AlbumViewModel? SelectedAlbum { get; set; }

    private async void DoSearch(string s)
    {
        IsBusy = true;
        SearchResults.Clear();

        _cancellationTokenSource?.Cancel();

        _cancellationTokenSource = new CancellationTokenSource();

        var albums = await _albumService.SearchAsync(s);

        foreach (var album in albums)
        {
            var vm = new AlbumViewModel(album);

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