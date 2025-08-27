using ALP.SceneGeneration.LevelData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALP.GameData.GameLevelData;

namespace ALP.SceneGeneration.Generations
{
    public interface ILevelGenerator
    {
        void GenerateLevel(LevelSO levelSO);
    }
}
