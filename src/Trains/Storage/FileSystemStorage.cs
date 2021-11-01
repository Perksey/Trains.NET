﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trains.NET.Engine;

namespace Trains.Storage;

public class FileSystemStorage : IGameStorage
{
    private const string TracksFilename = "Trains.NET.tracks";
    private const string TerrainFilename = "Trains.NET.terrain";

    private readonly string _tracksFilePath = GetFilePath(TracksFilename);
    private readonly string _terrainFilePath = GetFilePath(TerrainFilename);

    internal static string GetFilePath(string fileName)
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Trains.NET", fileName);
    }

    private readonly IEntityCollectionSerializer _entityCollectionSerializer;
    private readonly ITerrainSerializer _terrainSerializer;

    public FileSystemStorage(IEntityCollectionSerializer serializer, ITerrainSerializer terrainSerializer)
    {
        _entityCollectionSerializer = serializer;
        _terrainSerializer = terrainSerializer;
    }

    public IEnumerable<IEntity> ReadEntities()
    {
        if (!File.Exists(_tracksFilePath))
        {
            return Enumerable.Empty<IEntity>();
        }

        return _entityCollectionSerializer.Deserialize(File.ReadAllLines(_tracksFilePath));
    }

    public void WriteEntities(IEnumerable<IEntity> entities)
    {
        var tracksFileDirectory = Path.GetDirectoryName(_tracksFilePath);
        if (tracksFileDirectory != null)
        {
            Directory.CreateDirectory(tracksFileDirectory);
            File.WriteAllText(_tracksFilePath, _entityCollectionSerializer.Serialize(entities));
        }
    }

    public IEnumerable<Terrain> ReadTerrain()
    {
        if (!File.Exists(_terrainFilePath))
        {
            return Enumerable.Empty<Terrain>();
        }

        return _terrainSerializer.Deserialize(File.ReadAllLines(_terrainFilePath));
    }

    public void WriteTerrain(IEnumerable<Terrain> terrainList)
    {
        var terrainFileDirectory = Path.GetDirectoryName(_terrainFilePath);
        if (terrainFileDirectory != null)
        {
            Directory.CreateDirectory(terrainFileDirectory);
            File.WriteAllText(_terrainFilePath, _terrainSerializer.Serialize(terrainList));
        }
    }
}

