using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.Interactables
{
    public interface IObstacle
    {
        Vector3Int GridPosition { get; }
        Vector3 Position { get; }

        ObstacleSize ObstacleSize { get; }

        bool IsMoving { get; }
    }
}
