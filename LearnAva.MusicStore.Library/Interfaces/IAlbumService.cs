using System.Text.Json;
using iTunesSearch.Library;
using LearnAva.MusicStore.Library.Models;

namespace LearnAva.MusicStore.Library.Interfaces;

public interface IAlbumService
{
    public const string Cache = "./Cache";
    private static readonly iTunesSearchManager ITunesSearchManager = new();

    public Task<Stream> LoadCoverBitmapAsync(Album album);
    public Stream SaveCoverBitmapSteam(Album album);

    public Task SaveAsync(Album album);

    #region static methods

    public static async Task SaveToStreamAsync(Album data, Stream stream)
    {
        await JsonSerializer.SerializeAsync(stream, data).ConfigureAwait(false);
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

    public static async Task<IEnumerable<Album>> SearchAsync(string searchTerm)
    {
        var query = await ITunesSearchManager.GetAlbumsAsync(searchTerm).ConfigureAwait(false);

        return query.Albums.Select(x =>
            new Album(x.ArtistName, x.CollectionName, x.ArtworkUrl100.Replace("100x100bb", "600x600bb")));
    }

    #endregion
}