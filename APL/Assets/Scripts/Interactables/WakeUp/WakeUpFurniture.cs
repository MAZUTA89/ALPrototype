using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ALP.Interactables
{
    public class WakeUpFurniture : Furniture, IWakeupFurniture
    {
        public List<Vector3> ZonePositions { get; private set; }


        [Inject]
        public void Construct()
        {
        }
        public override void OnMouseStopDrag()
        {
            
        }
    }
}
