using System;
using UnityEngine;

namespace ALP.GameData.Camera
{
    [CreateAssetMenu(fileName = "CameraSO", menuName = "GameData/Camera/CameraSO")]
    public class CameraSO : ScriptableObject
    {
        [Range(1, 20)]
        public float Camera2dMovementSpeed;
        [Range(40, 80)]
        public float FOVMax;
        [Range(10, 40)]
        public float FOVMin;
        [Range(1, 100)]
        public float CameraUpDownSpeed;
    }
}
