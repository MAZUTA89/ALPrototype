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
        IEnumerable<Vector3Int> BoundsArea { get; }
        IEnumerable<Vector3Int> ExitArea { get; }
        Dictionary<Vector3Int, GameObject> BoundsObjects { get; }
        IEnumerable<Vector3Int> LightZoneArea { get; }

        void AddObstacle(IObstacle obstacle);
        void RemoveObstacle(IObstacle obstacle);
        void AddLightZonePosition(Vector3Int position);
        void RemoveLightZonePosition(Vector3Int position);
        void Initialize(IPlacementData placementData);
    }
}
