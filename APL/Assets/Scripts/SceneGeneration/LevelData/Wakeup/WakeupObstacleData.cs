using System;
using System.Collections.Generic;
using UnityEngine;

namespace ALP.SceneGeneration.LevelData
{
    public class WakeupObstacleData : MonoBehaviour
    {
        [SerializeField] private List<Outline> _zoneObjects;

        public List<Outline> ZoneObjects => _zoneObjects;
    }
}
