using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.SceneGeneration.LevelData
{
    public class TileMapData : ITileMapData
    {
        public Tilemap Tilemap => TileMap;

        protected GridLayout PrefabGrid;

        public IEnumerable<GameObject> MapChildrenObjects => MapObjects;

        public IEnumerable<Vector3Int> ObjectsGridPositions => GridPositions;

        protected IEnumerable<GameObject> MapObjects;
        protected IEnumerable<Vector3Int> GridPositions;

        protected Tilemap TileMap;

        public TileMapData(GridLayout gridLayout, Tilemap tilemap)
        {
            PrefabGrid = gridLayout;
            TileMap = tilemap;
            MapObjects = new List<GameObject>();
            GridPositions = new List<Vector3Int>();

            Initialize();
        }

        public void Initialize()
        {
            MapObjects = GetChildrenObjects();
            GridPositions = GetObjectsGridPositions();
        }

        public virtual IEnumerable<Vector3Int> GetObjectsGridPositions()
        {
            List<Vector3Int> positions = new List<Vector3Int>();

            GameObject tileMapObject = TileMap.gameObject;

            foreach (Transform transform in tileMapObject.transform)
            {
                Vector3Int objGridPos = PrefabGrid.WorldToCell(transform.position);

                positions.Add(objGridPos);
            }

            return positions;
        }

        public virtual IEnumerable<GameObject> GetChildrenObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            GameObject tileMapObject = TileMap.gameObject;

            foreach (Transform transform in tileMapObject.transform)
            {
                gameObjects.Add(transform.gameObject);
            }

            return gameObjects;
        }
    }
}
