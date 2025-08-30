using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using ALP.GameData.GameLevelData;
using ALP.SceneGeneration.Generations;
using ALP.SceneGeneration.LevelData;
using ALP.ALGridManagement;
using Cinemachine;
using ALP.CameraCode;

namespace ALP.Scripts.GameSceneInstallers
{
    public class GenerationInstaller : MonoInstaller
    {
        [SerializeField] ALCamera _gameCameraObject;
        [SerializeField] private LevelSO _levelSO;
        [SerializeField] private GridLayout _gameGridComponent;
        IGameGrid _gameGrid;

        ILevelGenerator _levelGenerator;
        public override void InstallBindings()
        {
            _gameGrid = new GameGrid(_gameGridComponent);


            _levelGenerator = new ALSceneGenerator(_gameGrid, Container, _gameCameraObject);


            _levelGenerator.GenerateLevel(_levelSO);


            Container.BindInstance(_gameGrid).AsSingle();
            Container.BindInstance(_levelGenerator).AsSingle();
            Container.BindInstance(_gameCameraObject).AsSingle();
        }
    }
}
