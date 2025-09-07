using ALP.ALGridManagement;
using ALP.CursorRay;
using ALP.Interactables;
using ALP.Leveling;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AL.ALGridManagement
{
    public class GridSystem
    {
        public IGridContainer GridContainer {  get; private set; }
        public GridCalculator Calculator { get; private set; }

        private ILevelSystem _levelSystem;

        IObstacle _lastMovedObstacle;

        public GridSystem(IGridContainer gameGrid, ALCursor aLCursor,
            ILevelSystem levelSystem)
        {
            GridContainer = gameGrid;
            Calculator = new GridCalculator(gameGrid);
            _levelSystem = levelSystem;
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
            bool isMoveAtExitArea = false;
            bool isMoveAtWakeupArea = false;

            if(_lastMovedObstacle is IPlayer player)
            {
                isMoveAtWakeupArea = HandleIfWakeupArea(player);

                isMoveAtExitArea = HandleIfExitArea(player);
            }    
            else
            {
                HandleIfLightZone(_lastMovedObstacle);
            }
            _lastMovedObstacle.OnEndMoveEvent -= OnEndMoveLastObstacle;

            if(isMoveAtExitArea == false && isMoveAtWakeupArea == false)
                _levelSystem.NextMove();
        }

        private void HandleIfLightZone(IObstacle movedObstacle)
        {
            if (Calculator.IsInLightZoneArea(movedObstacle, out Vector2Int cellPosition))
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

        private bool HandleIfWakeupArea(IPlayer player)
        {
            if (Calculator.IsInWakeupArea(_lastMovedObstacle.Position, out Vector2Int cellPosition))
            {
                player.Wakeup();

                IWakeupFurniture wakeupFurniture = GetWakeupFurniture(cellPosition);

                if (wakeupFurniture != null)
                {
                    wakeupFurniture.OnPlayerEnter();
                }

                return true;
            }

            return false;
        }
        private bool HandleIfExitArea(IPlayer player)
        {
            if(Calculator.IsInExitArea(_lastMovedObstacle.Position))
            {
                player.Exit();
                return true;
            }
            return false;
        }

        private IWakeupFurniture GetWakeupFurniture(Vector2Int cellPosition)
        {
            foreach (KeyValuePair<IWakeupFurniture, IEnumerable<Vector2Int>> keyValue 
                in GridContainer.WakeupObjects)
            {
                if(keyValue.Value.Contains(cellPosition))
                    return keyValue.Key;
            }

            return null;
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
