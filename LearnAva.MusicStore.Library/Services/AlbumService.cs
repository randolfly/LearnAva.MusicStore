using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Models;

namespace LearnAva.MusicStore.Library.Services;

public class AlbumService : IAlbumService
{
    private readonly HttpClient _httpClient = new();

    public async Task<Stream> LoadCoverBitmapAsync(Album album)
    {
        var cachePath = GetCachePath(album);
        if (File.Exists(cachePath + ".bmp"))
            return File.OpenRead(cachePath + ".bmp");

        var data = await _httpClient.GetByteArrayAsync(album.CoverUrl);

        return new MemoryStream(data);
    }

    public Stream SaveCoverBitmapSteam(Album album)
    {
        return File.OpenWrite(GetCachePath(album) + ".bmp");
    }

    public async Task SaveAsync(Album album)
    {
        if (!Directory.Exists(IAlbumService.Cache)) Directory.CreateDirectory(IAlbumService.Cache);

        await using var fs = File.OpenWrite(GetCachePath(album));
        await IAlbumService.SaveToStreamAsync(album, fs);
    }

    private static string GetCachePath(Album album)
    {
        var cachePath = $"{IAlbumService.Cache}/{album.Artist} - {album.Title}";
        return cachePath;
    }
}