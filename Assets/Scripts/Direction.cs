using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West,
    None
}

public static class DirectionMethods
{
    public static Direction Opposed(Direction d)
    {
        if (d == Direction.North)
            return Direction.South;
        if (d == Direction.South)
            return Direction.North;
        if (d == Direction.West)
            return Direction.East;
        if (d == Direction.East)
            return Direction.West;
        return Direction.None;
    }
}
