using ALP.SceneGeneration.LevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ALP.ALGridManagement
{
    public interface IGameGrid
    {
        GridLayout GameGridLayout { get; }
        Dictionary<Vector3Int, GameObject> ObstaclesObjects { get; }
        IEnumerable<Vector3Int> InteractableArea { get; }
        IEnumerable<Vector3Int> BoundsArea { get; }
        IEnumerable<Vector3Int> ExitArea { get; }
        Dictionary<Vector3Int, GameObject> BoundsObjects { get; }
        bool IsInteractableArea(Vector3Int position);
        bool IsExitArea(Vector3Int position);
        void Initialize(IPlacementData placementData);
    }
}
