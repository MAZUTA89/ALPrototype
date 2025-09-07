using ALP.Leveling;
using System;
using UnityEngine;
using Zenject;

namespace ALP.Interactables
{
    public class PlayerFurniture : Furniture, IPlayer
    {
        ILevelSystem _levelSystem;

        [Inject]
        public void Construct(ILevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
        }

        public void Exit()
        {
            _levelSystem.NextLevel();
        }

        public void Wakeup()
        {
            _levelSystem.RestartLevel();
        }
    }
}
