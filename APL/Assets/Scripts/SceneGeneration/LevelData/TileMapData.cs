using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.SceneGeneration.LevelData
{
    public class TileMapData : ITileMapData
    {
        public Tilemap Tilemap => _tileMap;

        GridLayout _prefabGrid;

        public IEnumerable<GameObject> MapChildrenObjects => _mapObjects;

        public IEnumerable<Vector3Int> ObjectsGridPositions => _gridPositions;

        List<GameObject> _mapObjects;
        List<Vector3Int> _gridPositions;

        private Tilemap _tileMap;

        public TileMapData(GridLayout gridLayout, Tilemap tilemap)
        {
            _prefabGrid = gridLayout;
            _tileMap = tilemap;
            _mapObjects = new List<GameObject>();
            _gridPositions = new List<Vector3Int>();

            Initialize();
        }

        public void Initialize()
        {
            GameObject tileMapObject = _tileMap.gameObject;

            foreach (Transform transform in tileMapObject.transform)
            {
                _mapObjects.Add(transform.gameObject);

                Vector3Int objGridPos = _prefabGrid.WorldToCell(transform.position);

                _gridPositions.Add(objGridPos);
            }
        }
    }
}
