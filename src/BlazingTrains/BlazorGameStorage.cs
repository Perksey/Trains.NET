﻿using Trains.NET.Engine;

namespace BlazingTrains;

public class BlazorGameStorage : IGameStorage
{
    public IServiceProvider? AspNetCoreServices { get; set; }

    //private ILocalStorageService? LocalStorageService => this.AspNetCoreServices?.GetService<ILocalStorageService>();

    public string? ReadEntities()
    {
        return null;
    }

    public void WriteEntities(string entities)
    {
    }
}
