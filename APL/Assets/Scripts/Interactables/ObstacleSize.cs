using System;
using System.Collections.Generic;
using UnityEngine;

namespace ALP.Interactables
{
    public class ObstacleSize
    {
        public SizeType SizeType { get; private set; }
        public Vector2 Size { get; private set; }

        protected Vector3 CenterPosition { get; private set; }

        public ObstacleSize(Vector2 size, SizeType sizeType, IObstacle obstacle)
        {
            Size = size;
            SizeType = sizeType;
        }


    }

    public enum SizeType
    {
        Cube,
        L
    }
}
