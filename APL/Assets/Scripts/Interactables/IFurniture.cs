using System;
using System.Collections.Generic;

namespace ALP.Interactables
{
    public interface IFurniture : IInteractable, IObstacle
    {
        event Action OnEndMoveEvent;
        void OnEnterLightZone();
    }
}
