using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ALP.Interactables
{
    public class LightZone : MonoBehaviour
    {
        public void OnEnterObstacle()
        {
            Destroy(gameObject);
        }
    }
}
