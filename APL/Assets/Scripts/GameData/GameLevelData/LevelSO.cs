using System;
using UnityEngine;

namespace ALP.GameData.GameLevelData
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "GameData/LevelBuilding/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] public GameObject LevelGridPrefab;
    }
}
