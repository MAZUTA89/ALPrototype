using UnityEngine;

namespace ALP.SceneGeneration.LevelData
{
    public interface IPlacementData
    {
        ITileMapData ObstaclesMapData { get; }
        ITileMapData BoundsMapData { get; }
        ITileMapData BoundAreaData { get; }
        ITileMapData InteractableAreaData { get; }
        ITileMapData ExitAreaData { get; }
        GridLayout BaseGrid { get; }
    }
}
