using ALP.ALGridManagement;
using ALP.GameData.GameLevelData;
using ALP.GameData.Leveling;
using ALP.SceneGeneration.Generations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Leveling
{
    public class LevelingSystem : ILevelSystem
    {
        LevelingListSO _levelingListSO;
        IGridContainer _gridContainer;
        ILevelGenerator _levelGenerator;
        int _lastLevelIndex;
        int _currentLevelIndex;

        public LevelingSystem(LevelingListSO levelingListSO,
            IGridContainer gridContainer,
            ILevelGenerator levelGenerator)
        {
            _gridContainer = gridContainer;
            _levelGenerator = levelGenerator;
            _levelingListSO = levelingListSO;
            _currentLevelIndex = 0;
        }

        public void ExecuteFirstLevel()
        {
            StartLevel(0);
        }
        public void NextLevel()
        {
            _gridContainer.Clear();

            if (_currentLevelIndex == _levelingListSO.Levels.Count)
                _currentLevelIndex = 0;

            _lastLevelIndex = _currentLevelIndex;

            StartLevel(_currentLevelIndex);

            _currentLevelIndex++;
        }

        public void RestartLevel()
        {
            _gridContainer.Clear();

            StartLevel(_lastLevelIndex);
        }

        void StartLevel(int levelIndex)
        {
            LevelSO levelSO = _levelingListSO.Levels[levelIndex];

            if(levelSO != null)
            {
                _levelGenerator.GenerateLevel(levelSO);
            }
        }
    }
}
