using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ALP.GameData.GameLevelData
{
    [CreateAssetMenu(fileName ="LevelTilesData", menuName = "GameData/LevelBuilding/LevelTilesData")]
    public class LevelTilesData : ScriptableObject
    {
        [SerializeField] public Tile BoundsTile;
        [SerializeField] public Tile InteractableTile;
        [SerializeField] public Tile ExitTile;
    }
}
