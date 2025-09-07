using System;
using UnityEngine;

namespace ALP.GameData.GameLevelData
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "GameData/Leveling/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] public string LevelName;
        [SerializeField] public GameObject LevelGridPrefab;
        [Range(1, 1000)]
        [SerializeField] public int LevelTurns;
    }
}
