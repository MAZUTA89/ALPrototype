using ALP.SceneGeneration.LevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ALP.ALGridManagement
{
    public class GameGrid : IGameGrid
    {
        public GridLayout GameGridLayout => _gameSceneGrid;

        public Dictionary<Vector3Int, GameObject> ObstaclesObjects => _obstaclesObjects;

        public Dictionary<Vector3Int, GameObject> BoundsObjects => _boundsObjects;

        GridLayout _gameSceneGrid;

        private Dictionary<Vector3Int, GameObject> _obstaclesObjects;
        private Dictionary<Vector3Int, GameObject> _boundsObjects;

        public GameGrid(GridLayout gameGrid)
        {
            _gameSceneGrid = gameGrid;
            _obstaclesObjects = new Dictionary<Vector3Int, GameObject>();
            _boundsObjects = new Dictionary<Vector3Int, GameObject>();
        }

        public void Initialize(IPlacementData placementData)
        {

        }
    }
}
