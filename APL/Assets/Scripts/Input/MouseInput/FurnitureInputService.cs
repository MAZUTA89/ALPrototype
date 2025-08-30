using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

namespace ALP.InputCode.MouseInput
{
    public class FurnitureInputService : InputService, IFurnitureInputService
    {
        public bool IsDragging { get; private set; }

        public event Action<InputAction.CallbackContext> OnStartDragEvent;
        public event Action<InputAction.CallbackContext> OnEndDragEvent;

        public FurnitureInputService(ALInput mainInputMap) 
            : base(mainInputMap)
        {
            MainMap.FurnitureInteractionMap.MoveFurniture.performed += OnStartDrag;
            MainMap.FurnitureInteractionMap.MoveFurniture.canceled += OnEndDrag;
        }

        private void OnEndDrag(InputAction.CallbackContext context)
        {
            OnEndDragEvent?.Invoke(context);
            IsDragging = false;
        }

        private void OnStartDrag(InputAction.CallbackContext context)
        {
            OnStartDragEvent?.Invoke(context);
            IsDragging = true;
        }

        public override void Enable()
        {
            MainMap.FurnitureInteractionMap.MoveFurniture.Enable();
            
        }

        public override void Disable()
        {
            MainMap.FurnitureInteractionMap.MoveFurniture.Disable();
        }

        public override void Dispose()
        {
            Disable();
            MainMap.FurnitureInteractionMap.MoveFurniture.performed -= OnStartDrag;
            MainMap.FurnitureInteractionMap.MoveFurniture.canceled -= OnEndDrag;
        }
    }
}
