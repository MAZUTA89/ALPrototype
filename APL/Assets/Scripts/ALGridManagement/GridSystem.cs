using ALP.ALGridManagement;
using ALP.CursorRay;
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

            //Vector3 gridMousePosition = _gameGrid.GameGridLayout.transform.InverseTransformPoint(mouseWorldPosition);

            //return gridMousePosition;
            return mouseWorldPosition;
        }


        public Vector3 SnapPositionToCell(Vector3 position)
        {
            Vector3Int cellPos = _gameGrid.GameGridLayout.LocalToCell(position);

            return _gameGrid.GameGridLayout.GetCellCenterLocal(cellPos);
        }

    }
}
