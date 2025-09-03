using AL.ALGridManagement;
using ALP.ALGridManagement;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace ALP.Interactables
{
    public class Furniture : MonoBehaviour, IFurniture
    {
        public Vector2Int[] FieldGridPos;
        public Vector2Int FieldGridPivotPos;
        [SerializeField] private SizeType _sizeType;
        public Vector3Int GridPosition { get; private set; }
        
        public Vector3 Position
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

        public ObstacleSize ObstacleSize { get; private set; }

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
            ObstacleSize = new ObstacleSize(_sizeType);
            UpdateGridPosition();
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawLine(_startDragPosition, _direction);
            if(Application.isPlaying)
                DrawOccupiedCells();
        }

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
            Vector3 targetPosition = _gridSystem.GetTargetPositionFromDirection(Position);
            Vector3 mousePosition = _gridSystem.GetMousePositionAtGrid();

            UpdateGridPosition();

            if (_gridSystem.IsCanPlace(mousePosition, targetPosition, this) == false)
            {
                Position = _startDragPosition;
                return;
            }

            _currentDragPosition = new Vector3(targetPosition.x, _startDragPosition.y, targetPosition.z);

            transform.position = _currentDragPosition;
            UpdateGridPosition();
        }

        public void OnDrag()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                DrawDirection();
            }
#endif

        }

        void UpdateGridPosition()
        {
            GridPosition = _gridSystem.SnapPositionToCellInt(Position);
            FieldGridPivotPos = new Vector2Int(GridPosition.x, GridPosition.y);
            FieldGridPos = ObstacleSize.GetGridPositions(GridPosition);
        }

        void DrawDirection()
        {
            _direction = _gridSystem.GetNearestDirection(Position);

            Vector3[] directions =
            {
                new Vector3(0, 0, 1),//up
                new Vector3(0, 0, -1),//down
                Vector3.left,
                Vector3.right
            };

            Debug.DrawRay(Position, directions[0], Color.red);
            Debug.DrawRay(Position, directions[1], Color.green);
            Debug.DrawRay(Position, directions[2], Color.blue);
            Debug.DrawRay(Position, directions[3], Color.magenta);
            Debug.DrawRay(Position, _direction, Color.gray);
        }

        void DrawOccupiedCells()
        {
            UpdateGridPosition();
            Vector2Int[] occupiedCells = ObstacleSize.GetGridPositions(GridPosition);

            foreach (var occupiedCell in occupiedCells)
            {
                Vector3 localGridPos = _gridSystem.GameGrid.GameGridLayout.GetCellCenterLocal(new Vector3Int(occupiedCell.x, occupiedCell.y, 0));

                Gizmos.DrawSphere(localGridPos, 0.2f);
            }
        }
    }
}
