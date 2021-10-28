﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Trains.NET.Engine
{
    public interface IGameBoard : IDisposable
    {
        bool Enabled { get; set; }
        int TerrainSeed { get; set; }
        IEnumerable<(Track, Train, float)> LastTrackLeases { get; }

        void ClearAll();
        IMovable? AddTrain(int column, int row);
        ImmutableList<IMovable> GetMovables();
        void RemoveMovable(IMovable thing);
        IEnumerable<T> GetMovables<T>() where T : IMovable;
        IMovable? GetMovableAt(int column, int row);
        IEnumerable<TrainPosition> GetNextSteps(Train train, float distanceToMove);
        void Initialize(int columns, int rows);
    }
}
