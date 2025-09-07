using System;
using UnityEngine;

namespace ALP.Interactables
{
    public interface IObstacle
    {
        event Action<Vector3> OnEndMoveEvent;
        GameObject ObstacleObject { get; }
        Vector3Int GridPosition { get; }
        Vector3 Position { get; }
        Vector3 StartDragPosition { get; }
        bool IsDrag { get; }
        ObstacleSize ObstacleSize { get; }
        bool IsMoving { get; }
        void MoveTo(Vector3 position);
    }
}
