using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using ALP.GameData.GameLevelData;
using ALP.SceneGeneration.Generations;
using ALP.SceneGeneration.LevelData;
using ALP.ALGridManagement;

namespace ALP.Scripts.GameSceneInstallers
{
    public class GenerationInstaller : MonoInstaller
    {
        [SerializeField] private LevelSO LevelSO;
        [SerializeField] private GridLayout _gameGridComponent;
        IGameGrid _gameGrid;

        ILevelGenerator _levelGenerator;
        public override void InstallBindings()
        {
            _gameGrid = new GameGrid(_gameGridComponent);


            _levelGenerator = new ALGenerator(_gameGrid, Container);


            _levelGenerator.GenerateLevel(LevelSO);
        }
    }
}
