using AL.ALGridManagement;
using ALP.ALGridManagement;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace ALP.Interactables
{
    [RequireComponent(typeof(Collider))]
    public class Furniture : MonoBehaviour, IFurniture
    {
        public Vector2Int[] FieldGridPos;
        public Vector2Int FieldGridPivotPos;
        [SerializeField] private SizeType _sizeType;
        [SerializeField] private float _moveTime = 0.7f;

        public event Action<Vector3> OnEndMoveEvent;

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
        protected Collider Collider { get; private set; }

        public GameObject ObstacleObject => gameObject;

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
            Collider = GetComponent<Collider>();
            Collider.isTrigger = false;
        }
        private void OnEnable()
        {
        }
        private void OnDisable()
        {
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                DrawOccupiedCells();
#endif
        }
        #endregion
        public virtual void OnMouseStartDrag()
        {
            _startDragPosition = transform.position;
        }
        public virtual void OnMouseStopDrag()
        {
            _gridSystem.MoveObstacle(this);
        }
        public virtual void OnDrag()
        {
            if (Application.isPlaying)
            {
                DrawDirection();
            }
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
                Vector3 localGridPos = _gridSystem.GridContainer.Grid.GetCellCenterLocal(
                    new Vector3Int(occupiedCell.x, occupiedCell.y, 0));

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

            OnEndMoveEvent?.Invoke(_targetMovePosition);
        }
    }
}
