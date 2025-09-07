using System;

namespace ALP.Leveling
{
    public interface ILevelSystem
    {
        int TotalMoves { get; }
        void ExecuteFirstLevel();
        void NextLevel();
        void RestartLevel();
        void NextMove();
    }
}
