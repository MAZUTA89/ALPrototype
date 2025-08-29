using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.Obstacles
{
    public interface IObstacle
    {
        Vector3Int Position { get; }

        void OnStartDrag();
        void OnEndDrag();
    }
}
