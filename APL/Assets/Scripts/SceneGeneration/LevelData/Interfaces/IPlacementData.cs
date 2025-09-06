using Cinemachine;
using UnityEngine;

namespace ALP.SceneGeneration.LevelData
{
    public interface IPlacementData
    {
        CinemachineVirtualCamera Camera { get; }
        ITileMapData ObstaclesMapData { get; }
        ITileMapData BoundAreaData { get; }
        ITileMapData InteractableAreaData { get; }
        ITileMapData ExitAreaData { get; }
        ITileMapData TriggerMapData { get; }
        ITileMapData WakeupData { get; }
        GridLayout BaseGrid { get; }
    }
}
