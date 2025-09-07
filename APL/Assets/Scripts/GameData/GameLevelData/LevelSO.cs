using System;
using UnityEngine;

namespace ALP.GameData.GameLevelData
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "GameData/Leveling/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] public GameObject LevelGridPrefab;
        [SerializeField] float LevelTurns;
    }
}
