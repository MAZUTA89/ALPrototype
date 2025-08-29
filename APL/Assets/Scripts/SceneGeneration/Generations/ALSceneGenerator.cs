using ALP.ALGridManagement;
using ALP.GameData.GameLevelData;
using ALP.SceneGeneration.LevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ALP.SceneGeneration.Generations
{
    public class ALSceneGenerator : ILevelGenerator
    {
        IGameGrid _gameGrid;
        IInstantiator _instantiator;

        public event Action OnSceneGenerated;

        public ALSceneGenerator(IGameGrid gameGrid, IInstantiator instantiator)
        {
            _gameGrid = gameGrid;
            _instantiator = instantiator;
        }
        public void GenerateLevel(LevelSO levelSO)
        {
            GameObject levelPrefab = levelSO.LevelGridPrefab;

            if(levelPrefab.TryGetComponent(out PlacementPrefabData prefabData))
            {
                prefabData.Initialize();
                PlaceBoundObjectsAtScene(prefabData);
                PlaceObstacleObjectsAtScene(prefabData);

                OnSceneGenerated?.Invoke();
            }
            else
            {
                Debug.LogError($"Can't find {prefabData.GetType().Name} at prefab {levelPrefab.name}");
                throw new Exception($"Can't find {prefabData.GetType().Name} at prefab {levelPrefab.name}");
            }
        }

        void PlaceBoundObjectsAtScene(PlacementPrefabData prefabData)
        {
            foreach (GameObject obj in prefabData.BoundsMapData.MapChildrenObjects)
            {
                Vector3 prefabPos = obj.transform.position;
                Quaternion rotation = obj.transform.rotation;

                _instantiator.InstantiatePrefab(obj, prefabPos, rotation,
                    _gameGrid.GameGridLayout.transform);
            }
        }

        void PlaceObstacleObjectsAtScene(PlacementPrefabData prefabData)
        {
            foreach (GameObject obj in prefabData.ObstaclesMapData.MapChildrenObjects)
            {
                Vector3 prefabPos = obj.transform.position;
                Quaternion rotation = obj.transform.rotation;

                _instantiator.InstantiatePrefab(obj, prefabPos, rotation,
                    _gameGrid.GameGridLayout.transform);
            }
        }

    }
}
