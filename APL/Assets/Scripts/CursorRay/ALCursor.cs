using ALP.CameraCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using ALP.Interactables;

namespace ALP.CursorRay
{
    public class ALCursor : MonoBehaviour
    {
        ALCamera _camera;

        [Inject]
        public void Construct(ALCamera camera)
        {
            _camera = camera;
        }

        #region UnityMethods
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                GameObject hitObj = hitInfo.collider.gameObject;

                if(hitObj.TryGetComponent(out IInteractable interactable))
                {
                    interactable.OnMouseClick();
                }
            }
        }
        #endregion
    }
}
