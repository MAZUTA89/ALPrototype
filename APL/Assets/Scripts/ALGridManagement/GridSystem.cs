using ALP.ALGridManagement;
using ALP.CursorRay;
using ALP.Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Android.Types;
using UnityEngine;

namespace AL.ALGridManagement
{
    public class GridSystem
    {
        IGameGrid _gameGrid;
        ALCursor _cursor;
        public Dictionary<CardinalDirection, Vector3> Directions;
        public Dictionary<CardinalDirection, Vector3> DirectionsInt;
        public GridSystem(IGameGrid gameGrid, ALCursor aLCursor)
        {
            _gameGrid = gameGrid;
            _cursor = aLCursor;
            Directions = new Dictionary<CardinalDirection, Vector3>();
            DirectionsInt = new Dictionary<CardinalDirection, Vector3>();

            Directions[CardinalDirection.Up] = new Vector3(0, 0, 1);
            Directions[CardinalDirection.Down] = new Vector3(0, 0, -1);
            Directions[CardinalDirection.Left] = Vector3.left;
            Directions[CardinalDirection.Right] = Vector3.right;
            DirectionsInt[CardinalDirection.Up] = Vector3Int.up;
            DirectionsInt[CardinalDirection.Down] = Vector3Int.down;
            DirectionsInt[CardinalDirection.Left] = Vector3Int.left;
            DirectionsInt[CardinalDirection.Right] = Vector3Int.right;
        }
        /// <summary>
        /// Текущее положение мыши игрока на сетке
        /// </summary>
        /// <returns></returns>
        public Vector3 GetMousePositionAtGrid()
        {
            Vector3 mouseWorldPosition = _cursor.GetMouseWorldPosition();

            return mouseWorldPosition;
        }


        public Vector3 SnapPositionToCell(Vector3 position)
        {
            Vector3Int cellPos = _gameGrid.GameGridLayout.LocalToCell(position);

            return _gameGrid.GameGridLayout.GetCellCenterLocal(cellPos);
        }
        public Vector3Int SnapPositionToCellInt(Vector3 position)
        {
            Vector3Int cellPos = _gameGrid.GameGridLayout.LocalToCell(position);

            return cellPos;
        }

        public bool IsPositionEmpty(Vector3 targetPosition, IObstacle obstacleToMove)
        {
            Vector3Int positionInt = SnapPositionToCellInt(targetPosition);

            Vector2Int[] toMovePositions = obstacleToMove.ObstacleSize.GetGridPositions(positionInt);

            ///Перебираем все препятствия
            foreach (IObstacle obstacle in _gameGrid.Obstacles)
            {
                ObstacleSize size = obstacle.ObstacleSize;

                Vector2Int[] obstaclePositions = size.GetGridPositions(obstacle.GridPosition);

                ///Перебираем все позиции занятые препятствиями
                foreach (Vector2Int obstaclePos in obstaclePositions)
                {
                    ///Если желаемая позиция пересекается с препятствием то занято
                    foreach (Vector2Int toMovePos in toMovePositions)
                    {
                        if (obstaclePos == toMovePos)
                            return false;
                    }
                }
            }

            return true;
        }

        public bool IsCanPlace(Vector3 mousePos, Vector3 cellPos, IObstacle toMove)
        {
            //if (IsInInteractableArea(toMove, cellPos) == false)
            //    return false;
            if(IsPositionEmpty(cellPos, toMove) == false)
                return false;
            if (mousePos == Vector3.zero)
                return false;

            return true;
        }
        /// <summary>
        /// Проверяем, вмещается ли перемещаемый объект
        /// в сетку
        /// </summary>
        /// <param name="obstacleToMove">Перемещаемый объект</param>
        /// <param name="targetPosition">Желаемая позиция</param>
        /// <returns></returns>
        public bool IsInInteractableArea(IObstacle obstacleToMove, Vector3 targetPosition)
        {
            Vector3Int targetPositionInt = SnapPositionToCellInt(targetPosition);

            Vector2Int[] obstaclePositions = obstacleToMove.ObstacleSize.GetGridPositions(targetPositionInt);

            int interactableCounter = 0;

            foreach (Vector2Int obstaclePos in obstaclePositions)
            {
                foreach (Vector3Int interactablePos in _gameGrid.InteractableArea)
                {
                    if(obstaclePos.x == interactablePos.x &&
                        obstaclePos.y == interactablePos.y)
                    {
                        interactableCounter++;
                        ///если все позиции перемещаемого объекта находятся на секте, то все ок
                        if(interactableCounter == obstaclePositions.Length)
                            return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// Получить направление мыши от передаваемой позиции
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector3 GetMouseDirectionFromPosition(Vector3 mousePosition, Vector3 position)
        {
            Vector3 direction = (mousePosition - position).normalized;

            return direction;
        }
        /// <summary>
        /// Получить ближайшее направление (Up, Down, Left, Right)
        /// от объекта к мыши
        /// </summary>
        /// <param name="direction">Текущее направление от объекта к мыши</param>
        /// <returns></returns>
        public Vector3 GetNearestDirection(Vector3 objectPosition)
        {
            Vector3 mousePosition = GetMousePositionAtGrid();

            Vector3 direction = GetMouseDirectionFromPosition(mousePosition, objectPosition);

            Vector3[] directions = Directions.Values.ToArray();

            float maxDot = -1;
            int nearestVector = -1;

            for (int i = 0; i < directions.Length; i++)
            {
                float dot = Vector3.Dot(directions[i], direction);

                if(dot > maxDot)
                {
                    nearestVector = i;
                    maxDot = dot;
                }
            }

            return directions[nearestVector];
        }
        /// <summary>
        /// Находится ли мышь игрока на игровой территории
        /// </summary>
        /// <returns></returns>
        public bool IsMouseAtInteractableGridArea()
        {
            Vector3 mousePosition = GetMousePositionAtGrid();

            Vector3 mouseAtCell = SnapPositionToCell(mousePosition);

            foreach (Vector3Int position in _gameGrid.InteractableArea)
            {
                if(position.x == mouseAtCell.x &&
                    position.y == mouseAtCell.y)
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Получить новую позицию объекта исходя из направления мыши
        /// </summary>
        /// <param name="currrentPosition"></param>
        /// <returns></returns>
        public Vector3 GetTargetPositionFromDirection(Vector3 currentPosition)
        {
            Vector3 targetDirection = GetNearestDirection(currentPosition);


            Vector3 targetPosition = targetDirection + currentPosition;

            targetPosition = SnapPositionToCell(targetPosition);

            return targetPosition;
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
