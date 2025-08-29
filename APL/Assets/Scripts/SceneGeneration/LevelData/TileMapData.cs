using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public int Count => PrefabCells.Count();

        public IEnumerable<PrefabCell> PrefabCells => ObjectsPrefabCells;

        protected IEnumerable<GameObject> MapObjects;
        protected IEnumerable<Vector3Int> GridPositions;
        protected List<PrefabCell> ObjectsPrefabCells;

        protected Tilemap TileMap;

        public TileMapData(GridLayout gridLayout, Tilemap tilemap)
        {
            PrefabGrid = gridLayout;
            TileMap = tilemap;
            MapObjects = new List<GameObject>();
            GridPositions = new List<Vector3Int>();
            ObjectsPrefabCells = new List<PrefabCell>();

            Initialize();
        }

        public virtual void Initialize()
        {
            MapObjects = GetChildrenObjects();
            GridPositions = GetObjectsGridPositions();

            for (int i = 0; i < GridPositions.Count(); i++)
            {
                PrefabCell prefabCell = new(MapObjects.ElementAt(i), GridPositions.ElementAt(i));

                ObjectsPrefabCells.Add(prefabCell);
            }
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
        public IEnumerator<PrefabCell> GetEnumerator()
        {
            return PrefabCells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return PrefabCells.GetEnumerator();
        }
    }
}
