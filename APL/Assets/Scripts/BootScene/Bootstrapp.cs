using ALP.Leveling;
using System;
using UnityEngine;
using Zenject;

namespace ALP.BootScene
{
    /// <summary>
    /// Стартуем отсюда
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        ILevelSystem _levelSystem;

        [Inject]
        public void Construct(ILevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
        }
        private void Start()
        {
            _levelSystem.ExecuteFirstLevel();
        }
    }
}
