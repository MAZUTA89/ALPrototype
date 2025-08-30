using ALP.InputCode;
using System;
using UnityEngine.InputSystem;

namespace ALP.InputCode.MouseInput
{
    public interface IFurnitureInputService : IInputService
    {
        event Action<InputAction.CallbackContext> OnStartDragEvent;
        event Action<InputAction.CallbackContext> OnEndDragEvent;
        bool IsDragging { get; }
    }
}
