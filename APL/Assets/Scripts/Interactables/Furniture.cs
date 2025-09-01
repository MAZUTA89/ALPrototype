using AL.ALGridManagement;
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

        [Inject]
        public void Construct(GridSystem gridSystem)
        {
            _gridSystem = gridSystem;
        }

        private void Start()
        {
            UpdateGridPosition();
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
           
        }

        void UpdateGridPosition()
        {
            GridPosition = _gridSystem.SnapPositionToCellInt(WorldPosition);
        }

        
    }
}
