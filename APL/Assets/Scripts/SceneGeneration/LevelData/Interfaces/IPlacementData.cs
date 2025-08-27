using UnityEngine;

namespace ALP.SceneGeneration.LevelData
{
    public interface IPlacementData
    {
        ITileMapData ObstaclesMapData { get; }
        ITileMapData BoundsMapData { get; }
        GridLayout BaseGrid { get; }
    }
}
