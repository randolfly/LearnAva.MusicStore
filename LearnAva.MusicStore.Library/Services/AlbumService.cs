using System.Text.Json;
using iTunesSearch.Library;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Models;

namespace LearnAva.MusicStore.Library.Services;

public class AlbumService : IAlbumService
{
    // we don't know if it is thread-safe, so don't create it as a static member
    private readonly iTunesSearchManager ITunesSearchManager = new();

    public Stream SaveCoverBitmapSteam(Album album)
    {
        return File.OpenWrite(IAlbumService.GetCachePath(album) + ".bmp");
    }

    public async Task SaveAsync(Album album)
    {
        if (!Directory.Exists(IAlbumService.Cache)) Directory.CreateDirectory(IAlbumService.Cache);

        await using var fs = File.OpenWrite(IAlbumService.GetCachePath(album));
        await SaveToStreamAsync(album, fs);
    }

    public async Task SaveToStreamAsync(Album data, Stream stream)
    {
        await JsonSerializer.SerializeAsync(stream, data).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Album>> SearchAsync(string searchTerm)
    {
        var query = await ITunesSearchManager.GetAlbumsAsync(searchTerm).ConfigureAwait(false);

        return query.Albums.Select(x =>
            new Album(x.ArtistName, x.CollectionName, x.ArtworkUrl100.Replace("100x100bb", "600x600bb")));
    }
}