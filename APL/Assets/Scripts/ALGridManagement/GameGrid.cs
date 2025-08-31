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
    public class GameGrid : IGameGrid
    {
        #region InterfaceVariables
        public Grid GameGridLayout => _grid;

        public Dictionary<Vector3Int, GameObject> ObstaclesObjects => _obstaclesObjects;

        public Dictionary<Vector3Int, GameObject> BoundsObjects => _boundsObjects;

        public IEnumerable<Vector3Int> InteractableArea => _interactablePositions;

        public IEnumerable<Vector3Int> BoundsArea => _boundsPositions;

        public IEnumerable<Vector3Int> ExitArea => _exitPositions;
        #endregion

        GridLayout _gameSceneGrid;
        Grid _grid;

        private Dictionary<Vector3Int, GameObject> _obstaclesObjects;
        private Dictionary<Vector3Int, GameObject> _boundsObjects;
        private List<Vector3Int> _interactablePositions;
        private List<Vector3Int> _boundsPositions;
        private List<Vector3Int> _exitPositions;

        public GameGrid(Grid gameGrid)
        {
            _grid = gameGrid;
            _obstaclesObjects = new Dictionary<Vector3Int, GameObject>();
            _boundsObjects = new Dictionary<Vector3Int, GameObject>();
        }

        public void Initialize(IPlacementData placementData)
        {
            
            ITileMapData obstacleData = placementData.ObstaclesMapData;

            foreach (var cell in obstacleData)
            {
                if (_obstaclesObjects.TryAdd(cell.GridPosition,
                    cell.Object) == false)
                {
                    Debug.LogError($"Не удается добавить данные препятствия уровня на" +
                    $" {cell.GridPosition} в объекте {cell.Object.name}");
                }
            }

            ITileMapData interactableData = placementData.InteractableAreaData;

            _interactablePositions = new List<Vector3Int>(interactableData.ObjectsGridPositions);

            ITileMapData exitData = placementData.ExitAreaData;

            _exitPositions = new List<Vector3Int>(exitData.ObjectsGridPositions);

        }

        public bool IsInteractableArea(Vector3Int position)
        {
            return _interactablePositions.Contains(position);
        }
        public bool IsExitArea(Vector3Int position)
        {
            return _exitPositions.Contains(position);
        }

        public Vector3 SnapToGridCellCenter(Vector3 worldPosition)
        {
            Vector3Int cellPos = _grid.WorldToCell(worldPosition);
            Vector3 cellCenter = _grid.GetCellCenterWorld(cellPos);
            return cellCenter;
        }
    }
}
