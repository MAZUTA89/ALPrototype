using ALP.GameData.GameLevelData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ALP.GameData.Leveling
{
    [CreateAssetMenu(fileName ="LevelingListSO", menuName = "GameData/Leveling/LevelingListSO")]
    public class LevelingListSO : ScriptableObject
    {
        [SerializeField] public List<LevelSO> Levels;
    }
}
