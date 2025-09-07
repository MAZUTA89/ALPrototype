using UnityEngine;

namespace ALP.Interactables
{
    public class ObstacleSize
    {
        public SizeType SizeType { get; private set; }
        public Vector2 GridPositions { get; private set; }
        
        public ObstacleSize(SizeType sizeType)
        {
            SizeType = sizeType;
        }

        public Vector2Int[] GetGridPositions(Vector3Int currentGridPosition)
        {
            Vector2Int current2dPos = new Vector2Int(currentGridPosition.x, currentGridPosition.y);
            switch (SizeType)
            {
                case SizeType.Horizontal2:
                    {
                        return new Vector2Int[]
                        {
                            current2dPos, current2dPos + Vector2Int.right
                        };
                    }
                case SizeType.Vertical2:
                    {
                        return new Vector2Int[]
                        {
                            current2dPos, current2dPos + Vector2Int.up
                        };
                    }
                case SizeType.Single:
                    {
                        return new Vector2Int[]
                        {
                            current2dPos
                        };
                    }
                case SizeType.Square2x2:
                    {
                        return new Vector2Int[]
                        {
                            current2dPos,
                            current2dPos + Vector2Int.up,
                            current2dPos + Vector2Int.right,
                            current2dPos + Vector2Int.up + Vector2Int.right
                        };
                    }
                case SizeType.Square2x3:
                    {
                        return new Vector2Int[]
                        {
                            current2dPos,
                            current2dPos + Vector2Int.up,
                            current2dPos + Vector2Int.up + Vector2Int.up,
                            current2dPos + Vector2Int.right,
                            current2dPos + Vector2Int.up + Vector2Int.up + Vector2Int.right
                        };
                    }
            }

            return default;
        }
    }

    public enum SizeType
    {
        Single,
        Horizontal2,
        Vertical2,
        Square2x2,
        Square2x3
    }
}
