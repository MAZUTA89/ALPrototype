using ALP.ALGridManagement;
using ALP.GameData.GameLevelData;
using ALP.GameData.Leveling;
using ALP.SceneGeneration.Generations;
using ALP.UI;

namespace ALP.Leveling
{
    public class LevelingSystem : ILevelSystem
    {
        public int TotalMoves {get; private set;}
        LevelingListSO _levelingListSO;
        IGridContainer _gridContainer;
        ILevelGenerator _levelGenerator;
        int _currentMoves;
        SceneUI _sceneUI;
        int _lastLevelIndex;
        int _currentLevelIndex;

        public LevelingSystem(LevelingListSO levelingListSO,
            IGridContainer gridContainer,
            ILevelGenerator levelGenerator,
            SceneUI sceneUI)
        {
            _gridContainer = gridContainer;
            _levelGenerator = levelGenerator;
            _levelingListSO = levelingListSO;
            _sceneUI = sceneUI;
            _currentLevelIndex = 0;
            _currentMoves = 0;
        }
        public void ExecuteFirstLevel()
        {
            StartLevel(0);
        }
        public void NextLevel()
        {
            _gridContainer.Clear();

            _currentLevelIndex++;

            if (_currentLevelIndex == _levelingListSO.Levels.Count)
                _currentLevelIndex = 0;

            _lastLevelIndex = _currentLevelIndex;

            StartLevel(_currentLevelIndex);

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

                TotalMoves = levelSO.LevelTurns;
                _currentMoves = 0;
                _sceneUI.SetLevelText(levelSO.LevelName);
                _sceneUI.SetCurrentTurns(TotalMoves);
            }
        }
        public void NextMove()
        {
            _currentMoves++;

            _sceneUI.SetCurrentTurns(TotalMoves - _currentMoves);

            if (_currentMoves == TotalMoves)
                RestartLevel();
        }
    }
}
