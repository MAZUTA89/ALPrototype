using System;
using System.Collections.Generic;
using UnityEngine;

namespace ALP.InputCode
{
    public class InputService : IInputService, IDisposable
    {
        public ALInput MainMap { get; private set; }

        public InputService(ALInput mainInputMap)
        {
            MainMap = mainInputMap;
            Enable();
        }

        public virtual void Disable()
        {
            MainMap.Disable();
        }

        public virtual void Dispose()
        {
            Disable();
        }

        public virtual void Enable()
        {
            MainMap.Enable();
        }
    }
}
