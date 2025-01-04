using System;
using UnityEngine;

public enum Direction
{
    North,East,South,West
}

public static class DirectionOrder {
    private static readonly Direction[] _primaryOrder = new[] { Direction.North, Direction.South, Direction.East, Direction.West };
    private static readonly Direction[] _reverseOrder = new[] { Direction.West, Direction.East, Direction.South, Direction.North };

    public static Direction[] GetPrimary() {
        return _primaryOrder;
    }

    public static Direction[] GetReverse()
    {
        return _reverseOrder;
    }
}

public static class DirectionExtensions
{
    static Quaternion[] _rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f)
    };

    

    public static Quaternion GetRotation(this Direction direction)
    {
        return _rotations[(int)direction];
    }

    public static float ToAngle(this Direction direction)
    {
        return direction switch
        {
            Direction.North => 0f,
            Direction.East => 90f,
            Direction.South => 180f,
            Direction.West => 270f,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Direction Inverse(this Direction direction) {
        return direction switch
        {
            Direction.North => Direction.South,
            Direction.East => Direction.West,
            Direction.South => Direction.North,
            Direction.West => Direction.East,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}
