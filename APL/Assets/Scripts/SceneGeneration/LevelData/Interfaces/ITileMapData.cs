using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.SceneGeneration.LevelData
{
    public interface ITileMapData
    {
       Tilemap Tilemap { get; }
       IEnumerable<GameObject> MapChildrenObjects { get; }
       IEnumerable<Vector3Int> ObjectsGridPositions { get; }
    }
}
