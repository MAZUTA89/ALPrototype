using ALP.GameData.GameLevelData;

namespace ALP.SceneGeneration.Generations
{
    public interface ILevelGenerator
    {
        void GenerateLevel(LevelSO levelSO);
    }
}
