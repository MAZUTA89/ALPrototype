using Cinemachine;
using UnityEngine;

namespace ALP.SceneGeneration.LevelData
{
    public interface IPlacementData
    {
        CinemachineVirtualCamera Camera { get; }
        ITileMapData ObstaclesMapData { get; }
        ITileMapData BoundsMapData { get; }
        ITileMapData BoundAreaData { get; }
        ITileMapData InteractableAreaData { get; }
        ITileMapData ExitAreaData { get; }
        GridLayout BaseGrid { get; }
    }
}
