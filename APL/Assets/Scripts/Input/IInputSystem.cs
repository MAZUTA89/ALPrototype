using System;
using System.Collections.Generic;
using UnityEngine;

namespace ALP.Input
{
    public interface IInputSystem
    {
        void Enable();
        void Disable();
        Vector2 GetWASDDirection();
        Vector2 MouseWheel();
    }
}
