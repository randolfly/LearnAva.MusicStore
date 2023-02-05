﻿using Avalonia;
using LearnAva.MusicStore.Library.Interfaces;
using LearnAva.MusicStore.Library.Services;
using Splat;

namespace LearnAva.MusicStore.Library;

public static class ServiceBootStrap
{
    /// <summary>
    ///     register the services in library
    /// </summary>
    public static void RegisterAppServices()
    {
        // config log service
        var logger = new DebugLogger { Level = LogLevel.Debug };
        Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));
        // config interface-service

        Locator.CurrentMutable.RegisterConstant<IAlbumService>(new AlbumService());
    }

    public static AppBuilder RegisterAppServices(this AppBuilder appBuilder)
    {
        RegisterAppServices();
        return appBuilder;
    }
}