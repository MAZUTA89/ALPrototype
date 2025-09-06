using ALP.Interactables;
using ALP.SceneGeneration.LevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

namespace ALP.ALGridManagement
{
    public class GridContainer : IGridContainer
    {
        #region InterfaceVariables
        public Grid Grid => _grid;

        public Dictionary<Vector3Int, GameObject> ObstaclesObjects => _obstaclesObjects;

        public Dictionary<Vector3Int, GameObject> BoundsObjects => _boundsObjects;

        public IEnumerable<Vector3Int> InteractableArea => _interactablePositions;

        public IEnumerable<Vector3Int> BoundsArea => _boundsPositions;

        public IEnumerable<Vector3Int> ExitArea => _exitPositions;

        public IEnumerable<IObstacle> Obstacles => _obstacles;
        public IEnumerable<Vector2Int> LightZoneArea => _lightZoneArea;

        public Dictionary<Vector2Int, LightZone> LightZones => _lightZoneObjects;

        public IEnumerable<Vector2Int> WakeupArea => _wakeupArea;

        public Dictionary<IWakeupFurniture, IEnumerable<Vector2Int>> WakeupObjects => _wakeupObjects;
        #endregion

        GridLayout _gameSceneGrid;
        Grid _grid;

        private Dictionary<Vector3Int, GameObject> _obstaclesObjects;
        private Dictionary<Vector3Int, GameObject> _boundsObjects;
        private Dictionary<Vector2Int, LightZone> _lightZoneObjects;
        private List<Vector3Int> _interactablePositions;
        private List<Vector3Int> _boundsPositions;
        private List<Vector3Int> _exitPositions;
        private List<IObstacle> _obstacles;
        private List<Vector2Int> _lightZoneArea;
        private Dictionary<IWakeupFurniture, IEnumerable<Vector2Int>> _wakeupObjects;
        private List<Vector2Int> _wakeupArea;

        public GridContainer(Grid gameGrid)
        {
            _grid = gameGrid;
            _obstaclesObjects = new Dictionary<Vector3Int, GameObject>();
            _boundsObjects = new Dictionary<Vector3Int, GameObject>();
            _obstacles = new List<IObstacle>();
            _lightZoneArea = new List<Vector2Int>();
            _lightZoneObjects = new Dictionary<Vector2Int, LightZone>();
            _wakeupArea = new List<Vector2Int>();
            _wakeupObjects = 
                new Dictionary<IWakeupFurniture, IEnumerable<Vector2Int>>();
        }

        public void Initialize(IPlacementData placementData)
        {
            ITileMapData interactableData = placementData.InteractableAreaData;

            _interactablePositions = new List<Vector3Int>(interactableData.ObjectsGridPositions);

            ITileMapData exitData = placementData.ExitAreaData;

            _exitPositions = new List<Vector3Int>(exitData.ObjectsGridPositions);

            ITileMapData triggersData = placementData.TriggerMapData;
        }

        public void AddObstacle(IObstacle obstacle)
        {
            _obstacles.Add(obstacle);
        }

        public void RemoveObstacle(IObstacle obstacle)
        {
            _obstacles.Remove(obstacle);
        }

        public void AddLightZonePosition(Vector2Int position)
        {
            _lightZoneArea.Add(position);
        }
        public void RemoveLightZonePosition(Vector2Int position)
        {
            _lightZoneArea.Remove(position);
        }

        public void AddWakeupPosition(Vector2Int position)
        {
            _wakeupArea.Add(position);
        }

        public void RemoveWakeupPosition(Vector2Int position)
        {
            _wakeupArea.Remove(position);
        }
    }
}
