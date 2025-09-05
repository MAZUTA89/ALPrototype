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
        [SerializeField] Tilemap _boundsAreaTilemap;
        [SerializeField] Tilemap _exitAreaTilemap;

        public ITileMapData ObstaclesMapData { get; protected set; }

        public GridLayout BaseGrid => _baseGrid;

        public ITileMapData BoundsMapData { get; protected set; }

        public ITileMapData BoundAreaData { get; protected set; }

        public ITileMapData InteractableAreaData { get; protected set; }

        public ITileMapData ExitAreaData { get; protected set; }
        public ITileMapData TriggerMapData { get; protected set; }

        public CinemachineVirtualCamera Camera => _camera;

        public void Initialize()
        {
            ObstaclesMapData = new TileMapData(_baseGrid, _obstaclesTilemap);
            BoundsMapData = new TileMapData(_baseGrid, _boundsTilemap);
            BoundAreaData = new SpritesTilemapData(_baseGrid, _boundsAreaTilemap);
            InteractableAreaData = new SpritesTilemapData(_baseGrid, _interactableTilemap);
            ExitAreaData = new SpritesTilemapData(_baseGrid, _exitAreaTilemap);
            TriggerMapData = new TileMapData(_baseGrid, _triggersTilemap);
        }
        protected virtual void Start()
        {
            
        }

        #region UnityMethods

        #endregion
    }
}
