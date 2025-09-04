using ALP.ALGridManagement;
using ALP.CursorRay;
using ALP.Interactables;
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
            }
        }

        public bool IsCanMoveObstacle(IObstacle obstacle, out Vector3 targetPosition)
        {
            targetPosition = Calculator.GetTargetPositionFromDirection(obstacle.Position);

            return Calculator.IsCanPlace(targetPosition, obstacle) &&
                !obstacle.IsMoving;
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
