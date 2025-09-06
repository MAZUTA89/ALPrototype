using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.SceneGeneration.LevelData
{
    public class PlacementPrefabData : MonoBehaviour, IPlacementData
    {
        [SerializeField] CinemachineVirtualCamera _camera;
        [SerializeField] GridLayout _baseGrid;
        [SerializeField] Tilemap _obstaclesTilemap;
        [SerializeField] Tilemap _triggersTilemap;
        [SerializeField] Tilemap _boundsTilemap;
        [SerializeField] Tilemap _interactableTilemap;
        [SerializeField] Tilemap _wakeupTilemap;
        [SerializeField] Tilemap _exitAreaTilemap;

        public ITileMapData ObstaclesMapData { get; protected set; }

        public GridLayout BaseGrid => _baseGrid;

        public ITileMapData BoundsMapData { get; protected set; }

        public ITileMapData BoundAreaData { get; protected set; }

        public ITileMapData InteractableAreaData { get; protected set; }

        public ITileMapData ExitAreaData { get; protected set; }
        public ITileMapData TriggerMapData { get; protected set; }

        public CinemachineVirtualCamera Camera => _camera;

        public ITileMapData WakeupData { get; protected set; }

        public void Initialize()
        {
            ObstaclesMapData = new TileMapData(_baseGrid, _obstaclesTilemap);
            BoundsMapData = new TileMapData(_baseGrid, _boundsTilemap);
            InteractableAreaData = new SpritesTilemapData(_baseGrid, _interactableTilemap);
            ExitAreaData = new SpritesTilemapData(_baseGrid, _exitAreaTilemap);
            TriggerMapData = new TileMapData(_baseGrid, _triggersTilemap);
            WakeupData = new WakeupMapData(_baseGrid, _wakeupTilemap);
        }
        protected virtual void Start()
        {
            
        }

        #region UnityMethods

        #endregion
    }
}
