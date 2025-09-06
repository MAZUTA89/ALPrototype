using ALP.Interactables;
using ALP.SceneGeneration.LevelData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ALP.ALGridManagement
{
    public interface IGridContainer
    {
        Grid Grid { get; }
        Dictionary<Vector3Int, GameObject> ObstaclesObjects { get; }
        IEnumerable<IObstacle> Obstacles { get; }
        IEnumerable<Vector3Int> InteractableArea { get; }
        IEnumerable<Vector3Int> ExitArea { get; }
        IEnumerable<Vector2Int> LightZoneArea { get; }
        Dictionary<Vector2Int, LightZone> LightZones { get; }
        Dictionary<IWakeupFurniture, IEnumerable<Vector2Int>> WakeupObjects { get; }
        IEnumerable <Vector2Int> WakeupArea { get; }

        void AddObstacle(IObstacle obstacle);
        void RemoveObstacle(IObstacle obstacle);
        void AddLightZonePosition(Vector2Int position);
        void RemoveLightZonePosition(Vector2Int position);
        void AddWakeupPosition(Vector2Int position);
        void RemoveWakeupPosition(Vector2Int position);
        void Initialize(IPlacementData placementData);
    }
}
