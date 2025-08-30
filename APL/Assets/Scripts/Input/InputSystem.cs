using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ALP.Input
{
    public class InputSystem : IInputSystem, IDisposable
    {
        ALInput _inputMap;
        public InputSystem()
        {
            _inputMap = new ALInput();
            Enable();
        }

        public void Disable()
        {
            _inputMap.Disable();
        }

        public void Dispose()
        {
            Disable();
        }

        public void Enable()
        {
            _inputMap.Enable();
        }

        public Vector2 GetWASDDirection()
        {
            return _inputMap.Actions.CameraMovement.ReadValue<Vector2>();
        }

        public Vector2 MouseWheel()
        {
            return _inputMap.Actions.CameraWheel.ReadValue<Vector2>();
        }
    }
}
