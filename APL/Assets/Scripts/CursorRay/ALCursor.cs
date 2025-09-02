using ALP.CameraCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using ALP.Interactables;
using ALP.InputCode.MouseInput;

namespace ALP.CursorRay
{
    public class ALCursor : MonoBehaviour
    {
        ALCamera _camera;

        IFurnitureInputService _furnitureInput;
        IInteractable _currentDragObject;

        [Inject]
        public void Construct(ALCamera camera,
            IFurnitureInputService furnitureInput)
        {
            _camera = camera;
            _furnitureInput = furnitureInput;
        }

        #region UnityMethods
        private void Start()
        {
            _furnitureInput.OnStartDragEvent += OnStartDrag;
            _furnitureInput.OnEndDragEvent += OnEndDrag;
        }
        private void OnDestroy()
        {
            _furnitureInput.OnStartDragEvent -= OnStartDrag;
            _furnitureInput.OnEndDragEvent -= OnEndDrag;
            _furnitureInput.Dispose();
        }
        private void Update()
        {
            if (_furnitureInput.IsDragging)
            {
                _currentDragObject?.OnDrag();
            }
        }
        #endregion

        private void OnStartDrag(InputAction.CallbackContext context)
        {
            if (_furnitureInput.IsDragging)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                GameObject hitObj = hitInfo.collider.gameObject;

                if (hitObj.TryGetComponent(out IInteractable interactable))
                {
                    _currentDragObject = interactable;
                    _currentDragObject.OnMouseStartDrag();
                }
            }
        }

        private void OnEndDrag(InputAction.CallbackContext context)
        {
            _currentDragObject?.OnMouseStopDrag();
            _currentDragObject = null;
        }

        public Vector3 GetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                //Debug.Log(hitInfo.collider.gameObject.name);
                return hitInfo.point;
            }
            else return Vector3.zero;
        }
    }
}
