using AL.ALGridManagement;
using ALP.Interactables;
using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace ALP.ALGridManagement
{
    public class GridCalculator
    {
        public IGridContainer GridContainer { get; private set; }

        public Dictionary<CardinalDirection, Vector3> Directions;
        public Dictionary<CardinalDirection, Vector3> DirectionsInt;

        public GridCalculator(IGridContainer gridContainer)
        {
            GridContainer = gridContainer;

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
            Vector3 mouseWorldPosition = GetMouseWorldPosition();

            return mouseWorldPosition;
        }

        public Vector3 GetMouseWorldPosition()
        {
            Plane plane = new Plane(Vector3.up, GridContainer.Grid.transform.position.y);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float enter))
            {
                return ray.GetPoint(enter);
            }
            else
                return Vector3.zero;

        }

        private Vector3 SnapPositionToCell(Vector3 position)
        {
            Vector3Int cellPos = GridContainer.Grid.LocalToCell(position);

            return GridContainer.Grid.GetCellCenterLocal(cellPos);
        }
        private Vector3Int SnapPositionToCellInt(Vector3 position)
        {
            Vector3Int cellPos = GridContainer.Grid.LocalToCell(position);

            return cellPos;
        }

        public bool IsPositionEmpty(Vector3 targetPosition, IObstacle obstacleToMove)
        {
            Vector3Int targetPositionInt = SnapPositionToCellInt(targetPosition);

            Vector2Int[] toMovePositions = obstacleToMove.ObstacleSize.GetGridPositions(targetPositionInt);

            HashSet<Vector2Int> occupiedCellsSet = new HashSet<Vector2Int>();

            ///Перебираем все препятствия
            foreach (IObstacle obstacle in GridContainer.Obstacles)
            {
                ///перемещаемый объект не учитываем
                if (obstacle == obstacleToMove)
                    continue;

                ObstacleSize size = obstacle.ObstacleSize;

                Vector3Int obstacleGridPosition = SnapPositionToCellInt(obstacle.Position);

                Vector2Int[] occupiedByObstacle = size.GetGridPositions(obstacleGridPosition);

                foreach (var occupiedCell in occupiedByObstacle)
                {
                    occupiedCellsSet.Add(occupiedCell);
                }
            }

            ///Ищем пересечения
            foreach (Vector2Int movePosition in toMovePositions)
            {
                if (occupiedCellsSet.Contains(movePosition))
                    return false;
            }

            return true;
        }
        /// <summary>
        /// Можно ли разместить перемещаемый объект
        /// </summary>
        /// <param name="mousePos"></param>
        /// <param name="targetPos"></param>
        /// <param name="toMove"></param>
        /// <returns></returns>
        public bool IsCanPlace(Vector3 targetPos, IObstacle toMove)
        {
            if (IsInInteractableArea(toMove, targetPos) == false)
                return false;
            if (IsPositionEmpty(targetPos, toMove) == false)
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
                foreach (Vector3Int interactablePos in GridContainer.InteractableArea)
                {
                    if (obstaclePos.x == interactablePos.x &&
                        obstaclePos.y == interactablePos.y)
                    {
                        interactableCounter++;
                        ///если все позиции перемещаемого объекта находятся на секте, то все ок
                        if (interactableCounter == obstaclePositions.Length)
                            return true;
                    }
                }
            }

            return false;
        }
        public bool IsInInteractableArea(Vector3 targetPosition)
        {
            targetPosition = GridContainer.Grid.transform.InverseTransformPoint(targetPosition);

            Vector3Int targetPositionInt = SnapPositionToCellInt(targetPosition);

            foreach (Vector3Int interactablePos in GridContainer.InteractableArea)
            {
                if (targetPositionInt.x == interactablePos.x &&
                    targetPositionInt.y == interactablePos.y)
                {
                    return true;
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

                if (dot > maxDot)
                {
                    nearestVector = i;
                    maxDot = dot;
                }
            }

            return directions[nearestVector];
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

            // targetPosition = SnapPositionToCell(targetPosition);

            return targetPosition;
        }

        public bool IsInLightZoneArea(Vector3 position, out Vector2Int cellPosition)
        {
            Vector3Int positionInt = SnapPositionToCellInt(position);

            Vector2Int position2Int = new Vector2Int(positionInt.x, positionInt.y);

            cellPosition = position2Int;

            return GridContainer.LightZoneArea.Contains(position2Int);
        }

        public bool IsInWakeupArea(Vector3 position, out Vector2Int cellPosition)
        {
            Vector3Int positionInt = SnapPositionToCellInt(position);

            Vector2Int position2Int = new Vector2Int(positionInt.x, positionInt.y);

            cellPosition = position2Int;

            return GridContainer.WakeupArea.Contains(position2Int);
        }

        public bool IsInExitArea(Vector3 position)
        {
            Vector3Int positionInt = SnapPositionToCellInt(position);

            Vector2Int position2Int = new Vector2Int(positionInt.x, positionInt.y);

            foreach (Vector3Int exitCell in GridContainer.ExitArea)
            {
                if(position2Int.x == exitCell.x &&
                    position2Int.y == exitCell.y)
                    return true;
            }

            return false;
        }
    }
}
