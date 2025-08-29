using ALP.GameData.GameLevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.SceneGeneration.LevelData
{
    public class SpritesTilemapData : TileMapData
    {
        protected LevelTilesData LevelTilesData;
        public SpritesTilemapData(GridLayout gridLayout,
            Tilemap tilemap)
            : base(gridLayout, tilemap)
        {
        }

        public override void Initialize()
        {
            MapObjects = GetChildrenObjects();
            GridPositions = GetObjectsGridPositions();

            for (int i = 0; i < GridPositions.Count(); i++)
            {
                PrefabCell prefabCell = new PrefabCell(null, ObjectsGridPositions.ElementAt(i));
            }
        }

        public override IEnumerable<Vector3Int> GetObjectsGridPositions()
        {
            List<Vector3Int> positions = new List<Vector3Int>();

            GameObject tileObject = Tilemap.gameObject;

            Vector3Int size2d = Tilemap.cellBounds.size;

            foreach (Vector3Int position in Tilemap.cellBounds.allPositionsWithin)
            {
                if (Tilemap.HasTile(position))
                {
                    positions.Add(position);
                }
            }

            return positions;
        }
    }
}
