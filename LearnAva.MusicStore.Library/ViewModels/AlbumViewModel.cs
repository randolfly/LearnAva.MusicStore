using Avalonia.Media.Imaging;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Models;
using LearnAva.MusicStore.Library.Services;
using ReactiveUI;
using Splat;

namespace LearnAva.MusicStore.Library.ViewModels;

public class AlbumViewModel : ViewModelBase
{
    private Bitmap? _cover;
    private readonly Album _album;

    [DependencyInjectionProperty]
    public IAlbumService AlbumService { get; set; }

    public AlbumViewModel(Album album)
    {
        _album = album;
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
        await using var imageStream = await AlbumService.LoadCoverBitmapAsync(_album);
        Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
    }

    public static async Task<IEnumerable<AlbumViewModel>> LoadCached()
    {
        // TODO 修改依赖注入方式，避免一直新建Service
        return (await IAlbumService.LoadCachedAsync()).Select(x => new AlbumViewModel(x));
    }

    public async Task SaveToDiskAsync()
    {
        await AlbumService.SaveAsync(_album);

        if (Cover != null)
        {
            await Task.Run(() =>
            {
                using var fs = AlbumService.SaveCoverBitmapSteam(_album);
                Cover.Save(fs);
            });
        }
    }
}