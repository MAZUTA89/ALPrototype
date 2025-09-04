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
using AL.ALGridManagement;

namespace ALP.Scripts.GameSceneInstallers
{
    public class GenerationInstaller : MonoInstaller
    {
        [SerializeField] ALCamera _gameCameraObject;
        [SerializeField] private LevelSO _levelSO;
        [SerializeField] private GridLayout _gameGridComponent;
        IGridContainer _gameGrid;

        ILevelGenerator _levelGenerator;
        public override void InstallBindings()
        {
            Grid grid = _gameGridComponent.GetComponent<Grid>();

            _gameGrid = new GridContainer(grid);

            _levelGenerator = new ALSceneGenerator(_gameGrid,
                Container, _gameCameraObject);

            Container.Bind<GridSystem>().AsSingle();
            Container.BindInstance(_gameGrid).AsSingle();
            Container.BindInstance(_levelGenerator).AsSingle();
            Container.BindInstance(_gameCameraObject).AsSingle();
        }

        public override void Start()
        {
            base.Start();
            
            _levelGenerator.GenerateLevel(_levelSO);
        }
    }
}
