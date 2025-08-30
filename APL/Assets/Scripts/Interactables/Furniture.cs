using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ALP.Interactables
{
    public class Furniture : IInteractable, IObstacle
    {
        public Vector3Int Position => throw new NotImplementedException();

        public void OnMouseClick()
        {
            throw new NotImplementedException();
        }

        public void OnMouseStartDrag()
        {
            throw new NotImplementedException();
        }

        public void OnMouseStopDrag()
        {
            throw new NotImplementedException();
        }
    }
}
