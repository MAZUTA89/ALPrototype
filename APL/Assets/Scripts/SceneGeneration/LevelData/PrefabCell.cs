using UnityEngine;

namespace ALP.SceneGeneration.LevelData
{
    public class PrefabCell
    {
        public GameObject Object { get; private set; }
        public Vector3Int GridPosition { get; private set; }
        public PrefabCell(GameObject obj, Vector3Int gridPosition)
        {
            Object = obj;
            GridPosition = gridPosition;
        }

    }
}
