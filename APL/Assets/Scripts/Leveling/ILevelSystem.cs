using System;

namespace ALP.Leveling
{
    public interface ILevelSystem
    {
        void ExecuteFirstLevel();
        void NextLevel();
        void RestartLevel();
    }
}
