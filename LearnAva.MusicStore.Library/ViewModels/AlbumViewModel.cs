using Avalonia.Media.Imaging;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Models;
using LearnAva.MusicStore.Library.Services;
using ReactiveUI;
using Splat;

namespace LearnAva.MusicStore.Library.ViewModels;

public class AlbumViewModel : ViewModelBase
{
    private readonly Album _album;
    private readonly IAlbumService _albumService;
    private Bitmap? _cover;

    public AlbumViewModel(Album album, IAlbumService? albumService = null)
    {
        _album = album;
        _albumService = albumService ?? Locator.Current.GetService<IAlbumService>() ?? new AlbumService();
    }

    public string Artist => _album.Artist;

    public string Title => _album.Title;

    public Bitmap? Cover
    {
        get => _cover;
        private set => this.RaiseAndSetIfChanged(ref _cover, value);
    }

    public async Task LoadCover()
    {
        await using var imageStream = await _albumService.LoadCoverBitmapAsync(_album);
        Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
    }

    public static async Task<IEnumerable<AlbumViewModel>> LoadCached()
    {
        // TODO �޸�����ע�뷽ʽ������һֱ�½�Service
        return (await IAlbumService.LoadCachedAsync()).Select(x => new AlbumViewModel(x));
    }

    public async Task SaveToDiskAsync()
    {
        await _albumService.SaveAsync(_album);

        if (Cover != null)
            await Task.Run(() =>
            {
                using var fs = _albumService.SaveCoverBitmapSteam(_album);
                Cover.Save(fs);
            });
    }
}