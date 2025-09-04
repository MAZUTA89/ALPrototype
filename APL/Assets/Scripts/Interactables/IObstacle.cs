using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.Interactables
{
    public interface IObstacle
    {
        Vector3Int GridPosition { get; }
        Vector3 Position { get; }
        Vector3 StartDragPosition { get; }
        bool IsDrag { get; }

        ObstacleSize ObstacleSize { get; }

        bool IsMoving { get; }

        void MoveTo(Vector3 position);
    }
}
