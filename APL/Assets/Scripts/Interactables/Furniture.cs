using AL.ALGridManagement;
using TMPro.EditorUtilities;
using UnityEngine;
using Zenject;

namespace ALP.Interactables
{
    public class Furniture : MonoBehaviour, IFurniture
    {
        public Vector3Int GridPosition { get; private set; }
        
        public Vector3 WorldPosition
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        GridSystem _gridSystem;

        Vector3 _startDragPosition;
        Vector3 _currentDragPosition;
        
        Vector3 _direction;

        [Inject]
        public void Construct(GridSystem gridSystem)
        {
            _gridSystem = gridSystem;
        }

        private void Start()
        {
            UpdateGridPosition();
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawLine(_startDragPosition, _direction);
        //}

        public void OnMouseClick()
        {
            Debug.Log(gameObject.name + " Clicked");
        }

        public void OnMouseStartDrag()
        {
            Debug.Log(gameObject.name + " Start drag");

            _startDragPosition = transform.position;

            Debug.Log(_startDragPosition);
        }

        public void OnMouseStopDrag()
        {
            Vector3 mousePosition = _gridSystem.GetMousePositionAtGrid();

            Vector3 atGridPosition = _gridSystem.SnapPositionToCell(mousePosition);

            UpdateGridPosition();

            if(_gridSystem.IsCanPlace(mousePosition, atGridPosition) == false)
            {
                WorldPosition = _startDragPosition;
                return;
            }

            _currentDragPosition = new Vector3(atGridPosition.x, _startDragPosition.y, atGridPosition.z);

            transform.position = _currentDragPosition;
        }

        public void OnDrag()
        {
            Vector3 mousePosition = _gridSystem.GetMousePositionAtGrid();

            //_direction = _gridSystem.GetMouseDirectionFromPosition(mousePosition, WorldPosition);

            //_direction = _gridSystem.GetNearestDirection(_direction);

            Vector3 targetPosition = _gridSystem.GetTargetPositionFromDirection(_currentDragPosition);

            Vector3 cellPosition = _gridSystem.SnapPositionToCell(targetPosition);

            transform.position = cellPosition;

            //Vector3[] directions =
            //{
            //    new Vector3(0, 0, 1),//up
            //    new Vector3(0, 0, -1),//down
            //    Vector3.left,
            //    Vector3.right
            //};

            //Debug.DrawRay(WorldPosition, directions[0], Color.red);
            //Debug.DrawRay(WorldPosition, directions[1], Color.green);
            //Debug.DrawRay(WorldPosition, directions[2], Color.blue);
            //Debug.DrawRay(WorldPosition, directions[3], Color.magenta);
            //Debug.DrawRay(WorldPosition, _direction, Color.gray);
            //Debug.Log(_direction);
        }

        void UpdateGridPosition()
        {
            GridPosition = _gridSystem.SnapPositionToCellInt(WorldPosition);
        }

        
    }
}
