using ALP.ALGridManagement;
using ALP.CursorRay;
using ALP.Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    obstacle.GridPosition.z == positionInt.z)
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

    }
}
