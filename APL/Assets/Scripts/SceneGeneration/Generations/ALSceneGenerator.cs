using ALP.ALGridManagement;
using ALP.CameraCode;
using ALP.GameData.GameLevelData;
using ALP.Interactables;
using ALP.SceneGeneration.LevelData;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ALP.SceneGeneration.Generations
{
    public class ALSceneGenerator : ILevelGenerator
    {
        IGridContainer _gameGrid;
        IInstantiator _instantiator;
        ALCamera _cameraObject;

        public ALSceneGenerator(IGridContainer gameGrid, IInstantiator instantiator,
            ALCamera cameraObject)
        {
            _gameGrid = gameGrid;
            _instantiator = instantiator;
            _cameraObject = cameraObject;
        }
        public void GenerateLevel(LevelSO levelSO)
        {
            GameObject levelPrefab = levelSO.LevelGridPrefab;

            if(levelPrefab.TryGetComponent(out PlacementPrefabData prefabData))
            {
                prefabData.Initialize();
                PlaceNotObstacleObjectsAtScene(prefabData.BoundsMapData.MapChildrenObjects);
                PlaceTriggersObjectsAtScene(prefabData);
                PlaceObstacleObjectsAtScene(prefabData);
                PlaceWakeupObjectsAtScene(prefabData);

                _cameraObject.Initialize(prefabData.Camera);

                _gameGrid.Initialize(prefabData);
            }
            else
            {
                Debug.LogError($"Can't find {prefabData.GetType().Name} at prefab {levelPrefab.name}");
                throw new Exception($"Can't find {prefabData.GetType().Name} at prefab {levelPrefab.name}");
            }
        }

        void PlaceNotObstacleObjectsAtScene(IEnumerable<GameObject> objects)
        {
            foreach (GameObject obj in objects)
            {
                Vector3 prefabPos = obj.transform.position;
                Quaternion rotation = obj.transform.rotation;

                GameObject gameObject = _instantiator.InstantiatePrefab(obj, prefabPos, rotation,
                    _gameGrid.Grid.transform);
            }
        }

        void PlaceTriggersObjectsAtScene(PlacementPrefabData prefabData)
        {
            foreach (PrefabCell prefabCell in prefabData.TriggerMapData)
            {
                GameObject instantObj = _instantiator.InstantiatePrefab(prefabCell.Object,
                    prefabCell.Object.transform.position,
                    prefabCell.Object.transform.rotation,
                    _gameGrid.Grid.transform);

                if(instantObj.TryGetComponent(out LightZone lightZone))
                {
                    _gameGrid.AddLightZonePosition(new Vector2Int(prefabCell.GridPosition.x,
                        prefabCell.GridPosition.y));

                    _gameGrid.LightZones.Add(
                        new Vector2Int(prefabCell.GridPosition.x, prefabCell.GridPosition.y),
                        lightZone);
                }
            }
        }

        void PlaceObstacleObjectsAtScene(PlacementPrefabData prefabData)
        {
            foreach (GameObject obj in prefabData.ObstaclesMapData.MapChildrenObjects)
            {
                Vector3 prefabPos = obj.transform.position;
                Quaternion rotation = obj.transform.rotation;

                GameObject instantObj = _instantiator.InstantiatePrefab(obj, prefabPos, rotation,
                    _gameGrid.Grid.transform);

                if(instantObj.TryGetComponent(out IObstacle obstacle))
                {
                    _gameGrid.AddObstacle(obstacle);
                }
            }
        }

        void PlaceWakeupObjectsAtScene(PlacementPrefabData prefabData)
        {
            WakeupMapData wakeupData = prefabData.WakeupData as WakeupMapData;

            foreach (GameObject obj in prefabData.WakeupData.MapChildrenObjects)
            {
                Vector3 prefabPos = obj.transform.position;
                Quaternion rotation = obj.transform.rotation;

                GameObject instantObj = _instantiator.InstantiatePrefab(obj, prefabPos, rotation,
                    _gameGrid.Grid.transform);
                
                if (instantObj.TryGetComponent(out IObstacle obstacle))
                {
                    _gameGrid.AddObstacle(obstacle);

                    ///Забираем wakeup объекты и их позиции из префаба в контейнер
                    if (obj.TryGetComponent(out IWakeupFurniture prefabFurniture))
                    {
                        if(obstacle is IWakeupFurniture furniture)
                        {
                            _gameGrid.WakeupObjects.Add(furniture, wakeupData.WakeupObjects[prefabFurniture]);
                        }
                    }
                }
            }

            ///Добавляем wakeup позиции в контейнер

            List<Vector3Int> gridPositions = 
                new List<Vector3Int>( 
                    prefabData.WakeupData.GetObjectsGridPositions());

            foreach (var pos in gridPositions)
            {
                _gameGrid.AddWakeupPosition(new Vector2Int(pos.x, pos.y));
            }
        }
    }
}
