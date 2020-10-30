﻿namespace Trains.NET.Engine
{
    public interface ITool
    {
        ToolMode Mode { get; }

        string Name { get; }

        void Execute(int column, int row, bool isPartOfDrag);

        bool IsValid(int column, int row);
    }
}
