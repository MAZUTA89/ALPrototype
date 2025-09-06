using ALP.Interactables;
using ALP.SceneGeneration.LevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.SceneGeneration.LevelData
{
    public class WakeupMapData : TileMapData
    {
        List<WakeupObstacleData> _wakeupDatas;
        public Dictionary<IWakeupFurniture, IEnumerable<Vector2Int>> WakeupObjects; 
        public WakeupMapData(GridLayout gridLayout,
            Tilemap tilemap) :
            base(gridLayout, tilemap)
        {
            
        }

        public override void Initialize()
        {
            _wakeupDatas = new List<WakeupObstacleData>();

            WakeupObjects = new Dictionary<IWakeupFurniture,
                IEnumerable<Vector2Int>>();

            MapObjects = GetChildrenObjects();

            foreach (var obj in MapObjects)
            {
                if(obj.TryGetComponent(out WakeupObstacleData wakeupObstacleData))
                {
                    _wakeupDatas.Add(wakeupObstacleData);
                }    
            }

            GridPositions = GetObjectsGridPositions();
        }

        public override IEnumerable<Vector3Int> GetObjectsGridPositions()
        {
            List<Vector3Int> allPositionsInt = new List<Vector3Int>();

            List<Vector2Int> wakupObjectPositions = null;

            foreach (WakeupObstacleData data in _wakeupDatas)
            {
                wakupObjectPositions = new List<Vector2Int>();

                if (data.gameObject.TryGetComponent(out IWakeupFurniture furniture))
                {
                    foreach (var obj in data.ZoneObjects)
                    {
                        Vector3Int position = PrefabGrid.WorldToCell(obj.transform.position);

                        allPositionsInt.Add(position);
                        wakupObjectPositions.Add(new Vector2Int(position.x, position.y));
                    }

                    WakeupObjects[furniture] = wakupObjectPositions;
                }
            }

            return allPositionsInt;
        }
    }
}
