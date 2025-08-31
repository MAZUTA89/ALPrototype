using AL.ALGridManagement;
using ALP.ALGridManagement;
using ALP.CursorRay;
using ALP.InputCode.MouseInput;
using System;
using UnityEngine;
using Zenject;

namespace ALP.Interactables
{
    public class Furniture : MonoBehaviour, IFurniture
    {
        public Vector3Int GridPosition { get; private set; }

        public Vector3 WorldPosition { get; private set; }

        GridSystem _gridSystem;

        Vector3 _startDragPosition;

        [Inject]
        public void Construct(GridSystem gridSystem)
        {
            _gridSystem = gridSystem;
        }

        private void Start()
        {
            WorldPosition = transform.position;
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
            //Debug.Log(gameObject.name + " End drag");
        }

        public void OnDrag()
        {
            Vector3 mousePosition = _gridSystem.GetMousePositionAtGrid();

            Vector3 atGridPosition = _gridSystem.SnapPositionToCell(mousePosition);
            
            transform.position = new Vector3(atGridPosition.x, _startDragPosition.y, atGridPosition.z);
        }
    }
}
