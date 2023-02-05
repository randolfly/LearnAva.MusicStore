﻿using Avalonia;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Services;
using Splat;

namespace LearnAva.MusicStore.Library;

public static class ServiceBootStrap
{
    /// <summary>
    /// register the services in library
    /// </summary>
    public static void RegisterLibraryServices()
    {
        SplatRegistrations.RegisterLazySingleton<IAlbumService, AlbumService>();

        SplatRegistrations.SetupIOC();
    }
}
