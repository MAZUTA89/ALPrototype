using ALP.ALGridManagement;
using ALP.CursorRay;
using ALP.Interactables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace AL.ALGridManagement
{
    public class GridSystem
    {
        public IGridContainer GridContainer {  get; private set; }
        public GridCalculator Calculator { get; private set; }

        IObstacle _lastMovedObstacle;

        public GridSystem(IGridContainer gameGrid, ALCursor aLCursor)
        {
            GridContainer = gameGrid;
            Calculator = new GridCalculator(gameGrid);
        }

        public void MoveObstacle(IObstacle obstacle)
        {
            if (IsCanMoveObstacle(obstacle, out Vector3 targetPosition))
            {
                targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);

                obstacle.MoveTo(targetPosition);

                _lastMovedObstacle = obstacle;

                _lastMovedObstacle.OnEndMoveEvent += OnEndMoveLastObstacle;
            }
        }

        public bool IsCanMoveObstacle(IObstacle obstacle, out Vector3 targetPosition)
        {
            targetPosition = Calculator.GetTargetPositionFromDirection(obstacle.Position);

            return Calculator.IsCanPlace(targetPosition, obstacle) &&
                !obstacle.IsMoving;
        }

        private void OnEndMoveLastObstacle(Vector3 movedPosition)
        {
            if(_lastMovedObstacle is not IPlayer player)
                HandleLightZone(movedPosition);
            
            _lastMovedObstacle.OnEndMoveEvent -= OnEndMoveLastObstacle;
        }

        private void HandleLightZone(Vector3 movedPosition)
        {
            if (Calculator.IsInLightZoneArea(movedPosition, out Vector2Int cellPosition))
            {
                LightZone lightZone = GridContainer.LightZones[cellPosition];

                ///Удаляем зону света
                GridContainer.LightZones.Remove(cellPosition);

                GridContainer.RemoveLightZonePosition(cellPosition);

                ///Удаляем объект
                lightZone.OnEnterObstacle();

                GameObject.Destroy(_lastMovedObstacle.ObstacleObject);

                GridContainer.RemoveObstacle(_lastMovedObstacle);

                
            }
        }
    }

    public enum CardinalDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
