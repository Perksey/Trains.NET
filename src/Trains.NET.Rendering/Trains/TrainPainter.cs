﻿using System;
using System.Collections.Generic;
using Trains.NET.Engine;

namespace Trains.NET.Rendering.Trains;

public class TrainPainter : ITrainPainter
{
    private readonly Dictionary<Guid, TrainPalette> _paletteMap = new();
    private readonly Random _random = new();

    private static readonly TrainPalette s_baseTrainPalette = new(
        Colors.Black,
        Colors.VeryDarkGray,
        Colors.Gray,
        Colors.DarkBlue, // Had to pick one, blue won out!
        Colors.LightBlue // This is never used though.
    );

    public TrainPalette GetPalette(Train train)
    {
        if (!_paletteMap.ContainsKey(train.UniqueID))
        {
            _paletteMap.Add(train.UniqueID, GetRandomPalette());
        }
        return _paletteMap[train.UniqueID];
    }

    private TrainPalette GetRandomPalette()
    {
        byte sR = (byte)_random.Next(32, 192);
        byte sG = (byte)_random.Next(32, 192);
        byte sB = (byte)_random.Next(32, 192);

        byte eR = (byte)(sR + 64);
        byte eG = (byte)(sG + 64);
        byte eB = (byte)(sB + 64);

        return s_baseTrainPalette with
        {
            FrontSectionStartColor = RGBToColor(sR, sG, sB),
            FrontSectionEndColor = RGBToColor(eR, eG, eB)
        };
    }

    private static Color RGBToColor(byte r, byte g, byte b)
        => new("#" + BitConverter.ToString(new[] { r, g, b }).Replace("-", string.Empty));
}
