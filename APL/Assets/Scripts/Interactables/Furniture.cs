using AL.ALGridManagement;
using ALP.ALGridManagement;
using System.Collections;
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
        [SerializeField] private float _moveTime = 1f;
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

        public bool IsMoving {  get; private set; }

        public Vector3 StartDragPosition { get; private set; }

        public bool IsDrag {  get; private set; }

        GridSystem _gridSystem;

        Vector3 _startDragPosition;
        Vector3 _currentDragPosition;
        Vector3 _targetMovePosition;
        Vector3 _direction;

        [Inject]
        public void Construct(GridSystem gridSystem)
        {
            _gridSystem = gridSystem;
        }

        #region UnityMethods
        private void Start()
        {
            ObstacleSize = new ObstacleSize(_sizeType);
        }

        private void Update()
        {
            
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
                DrawOccupiedCells();
        }
        #endregion


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
            //Vector3 targetPosition = _gridSystem.GetTargetPositionFromDirection(Position);
            //Vector3 mousePosition = _gridSystem.GetMousePositionAtGrid();

            //if (_gridSystem.IsCanPlace(mousePosition, targetPosition, this) == false)
            //{
            //    Position = _startDragPosition;
            //    return;
            //}

            //_currentDragPosition = new Vector3(targetPosition.x, _startDragPosition.y, targetPosition.z);

            //transform.position = _currentDragPosition;

            _gridSystem.MoveObstacle(this);

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

        void DrawDirection()
        {
            _direction = _gridSystem.Calculator.GetNearestDirection(Position);

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
            Vector2Int[] occupiedCells = ObstacleSize.GetGridPositions(GridPosition);

            foreach (var occupiedCell in occupiedCells)
            {
                Vector3 localGridPos = _gridSystem.GridContainer.Grid.GetCellCenterLocal(new Vector3Int(occupiedCell.x, occupiedCell.y, 0));

                Gizmos.DrawSphere(localGridPos, 0.2f);
            }
        }

        public void MoveTo(Vector3 position)
        {
            IsMoving = true;
            _targetMovePosition = position;

            StartCoroutine(MoveObstacleCoroutine());
        }

        private IEnumerator MoveObstacleCoroutine()
        {
            Vector3 startPosition = transform.position;

            float elapsedTime = 0f;

            while(elapsedTime < _moveTime)
            {
                transform.position = Vector3.Lerp(startPosition, _targetMovePosition,
                    elapsedTime / _moveTime);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            transform.position = _targetMovePosition;

            IsMoving = false;
        }
    }
}
