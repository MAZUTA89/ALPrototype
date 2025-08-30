using System;
using System.Collections.Generic;

namespace ALP.Interactables
{
    public interface IInteractable
    {
        void OnMouseClick();
        void OnMouseStartDrag();
        void OnMouseStopDrag();
    }
}
