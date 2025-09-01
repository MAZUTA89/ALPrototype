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
        public GridSystem(IGameGrid gameGrid, ALCursor aLCursor)
        {
            _gameGrid = gameGrid;
            _cursor = aLCursor;
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

        public bool IsPositionEmpty(Vector3 position)
        {
            Vector3Int positionInt = SnapPositionToCellInt(position);

            foreach (IObstacle obstacle in _gameGrid.Obstacles)
            {
                if (obstacle.GridPosition.x == positionInt.x &&
                    obstacle.GridPosition.y == positionInt.y)
                    return false;
            }

            return true;
        }

        public bool IsCanPlace(Vector3 mousePos, Vector3 cellPos)
        {
            if(IsPositionEmpty(cellPos) == false)
                return false;
            if (mousePos == Vector3.zero)
                return false;

            return true;
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

            Vector3[] directions =
           {
                new Vector3(0, 0, 1),//up
                new Vector3(0, 0, -1),//down
                Vector3.left,
                Vector3.right
            };

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
}
