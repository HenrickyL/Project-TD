
using System;

public enum Direction
{
    North,East,South,West
}


public static class DirectionExtensions
{
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
}
