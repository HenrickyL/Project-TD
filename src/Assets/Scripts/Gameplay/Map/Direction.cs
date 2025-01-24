using System;
using UnityEngine;

public enum Direction
{
    North,East,South,West
}

public enum DirectionChange
{
    None, TurnRight, TurnLeft, TurnAround
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
    static Vector3[] halfVectors = {
        Vector3.forward * 0.5f,
        Vector3.right * 0.5f,
        Vector3.back * 0.5f,
        Vector3.left * 0.5f
    };

    /* ------------------------------------------ */
    public static Vector3 GetVector(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Vector3.forward,
            Direction.East => Vector3.right,
            Direction.South => Vector3.back,
            Direction.West => Vector3.left,
            _ => Vector3.zero
        };
    }
    public static Vector3 GetHalfVector(this Direction direction)
    {
        return halfVectors[(int)direction];
    }

    public static Quaternion GetRotation(this Direction direction)
    {
        return _rotations[(int)direction];
    }


    public static float GetAngle(this Direction direction)
    {
        return (float)direction * 90f;
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

    public static DirectionChange GetDirectionChangeTo(
        this Direction current, Direction next
    )
    {
        if (current == next)
        {
            return DirectionChange.None;
        }
        else if (current + 1 == next || current - 3 == next)
        {
            return DirectionChange.TurnRight;
        }
        else if (current - 1 == next || current + 3 == next)
        {
            return DirectionChange.TurnLeft;
        }
        return DirectionChange.TurnAround;
    }
}
