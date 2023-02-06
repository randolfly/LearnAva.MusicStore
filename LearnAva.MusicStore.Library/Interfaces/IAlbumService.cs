using System.Text.Json;
using LearnAva.MusicStore.Library.Models;

namespace LearnAva.MusicStore.Library.Interfaces;

public interface IAlbumService
{
    public const string Cache = "./Cache";

    // thread safe, make static
    private static readonly HttpClient _httpClient = new();

    public Stream SaveCoverBitmapSteam(Album album);
    public Task SaveAsync(Album album);
    public Task SaveToStreamAsync(Album data, Stream stream);
    public Task<IEnumerable<Album>> SearchAsync(string searchTerm);

    #region static methods (used in viewmodels to provide static methods in viewmodels)

    public static async Task<Stream> LoadCoverBitmapAsync(Album album)
    {
        var cachePath = GetCachePath(album);
        if (File.Exists(cachePath + ".bmp"))
            return File.OpenRead(cachePath + ".bmp");

        var data = await _httpClient.GetByteArrayAsync(album.CoverUrl);

        return new MemoryStream(data);
    }

    public static async Task<Album> LoadFromStream(Stream stream)
    {
        return (await JsonSerializer.DeserializeAsync<Album>(stream).ConfigureAwait(false))!;
    }

    public static async Task<IEnumerable<Album>> LoadCachedAsync()
    {
        if (!Directory.Exists(Cache)) Directory.CreateDirectory(Cache);
        var results = new List<Album>();

        foreach (var file in Directory.EnumerateFiles(Cache))
        {
            if (!string.IsNullOrWhiteSpace(new DirectoryInfo(file).Extension)) continue;

            await using var fs = File.OpenRead(file);
            results.Add(await LoadFromStream(fs).ConfigureAwait(false));
        }

        return results;
    }

    internal static string GetCachePath(Album album)
    {
        var cachePath = $"{Cache}/{album.Artist} - {album.Title}";
        return cachePath;
    }

    #endregion
}