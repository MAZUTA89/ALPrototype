using System;
using UnityEngine;

namespace ALP.InputCode.CameraInput
{
    public class CameraInputService : InputService, ICameraInputService
    {
        public CameraInputService(ALInput mainInputMap)
            : base(mainInputMap)
        {
        }

        Vector2 ICameraInputService.GetWASDDirection()
        {
            return MainMap.CameraMap.CameraMovement.ReadValue<Vector2>();
        }

        Vector2 ICameraInputService.MouseWheel()
        {
            return MainMap.CameraMap.CameraWheel.ReadValue<Vector2>();
        }

        public override void Enable()
        {
            MainMap.CameraMap.CameraMovement.Enable();
            MainMap.CameraMap.CameraWheel.Enable();
        }

        public override void Dispose()
        {
            MainMap.CameraMap.CameraMovement.Disable();
            MainMap.CameraMap.CameraWheel.Disable();
        }
    }
}
