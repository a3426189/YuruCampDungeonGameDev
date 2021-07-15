using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonClawer
{
    public Vector2Int Position;

    public DungeonClawer(Vector2Int vector2Int)
    {
        this.Position = vector2Int;
    }

    public Vector2Int move(Dictionary<Direction,Vector2Int> directionMovementMap)
    {
        Direction RandomMove = (Direction)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[RandomMove];
        return Position;
    }

}
