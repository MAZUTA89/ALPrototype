using System;
using System.Collections.Generic;
using UnityEngine;

namespace ALP.InputCode.CameraInput
{
    public interface ICameraInputService : IInputService
    {
        Vector2 GetWASDDirection();
        Vector2 MouseWheel();
    }
}
