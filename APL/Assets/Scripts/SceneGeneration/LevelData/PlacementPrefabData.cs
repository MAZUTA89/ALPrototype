using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.SceneGeneration.LevelData
{
    public class PlacementPrefabData : MonoBehaviour, IPlacementData
    {
        [SerializeField] GridLayout _baseGrid;
        [SerializeField] Tilemap _obstaclesTileMap;
        [SerializeField] Tilemap _boundsTileMap;

        public ITileMapData ObstaclesMapData { get; protected set; }

        public GridLayout BaseGrid => _baseGrid;

        public ITileMapData BoundsMapData { get; protected set; }


        public void Initialize()
        {
            ObstaclesMapData = new TileMapData(_baseGrid, _obstaclesTileMap);
            BoundsMapData = new TileMapData(_baseGrid, _boundsTileMap);
        }
        protected virtual void Start()
        {
            
        }

        #region UnityMethods

        #endregion
    }
}
