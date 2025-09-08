using Zenject;
using UnityEngine;
using ALP.GameData.GameLevelData;
using ALP.SceneGeneration.Generations;
using ALP.ALGridManagement;
using ALP.CameraCode;
using AL.ALGridManagement;
using ALP.Leveling;
using ALP.GameData.Leveling;
using ALP.BootScene;

namespace ALP.Scripts.GameSceneInstallers
{
    public class GenerationInstaller : MonoInstaller
    {
        [SerializeField] private LevelingListSO _levelingListSO;
        [SerializeField] private ALCamera _gameCameraObject;
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

            Container.BindInstance(_levelingListSO)
                .AsSingle();

            Container.Bind<GridSystem>().AsSingle();
            Container.BindInstance(_gameGrid).AsSingle();
            Container.BindInstance(_levelGenerator).AsSingle();
            Container.BindInstance(_gameCameraObject).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelingSystem>()
                .AsSingle();

            Container.Bind<Bootstrap>()
                .FromComponentInHierarchy(true)
                .AsSingle();
        }
    }
}
