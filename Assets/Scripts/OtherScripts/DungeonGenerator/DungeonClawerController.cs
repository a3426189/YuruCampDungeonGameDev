using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DungeonGeneration 包括 人數(Clawer數) 
 * 
 * DungeonClawer 採取行動(蓋房子)
 * DungeonClawerController (負責處理資料)
 */
public enum Direction
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3,
}

public class DungeonClawerController : MonoBehaviour
{
    private static readonly Dictionary<Direction, Vector2Int> DirectionMovementMap = new Dictionary<Direction, Vector2Int>()
    {
        {Direction.Up,Vector2Int.up},
        {Direction.Left,Vector2Int.left},
        {Direction.Down,Vector2Int.down},
        {Direction.Right,Vector2Int.right},
    };
    //maybe need static ?
    public static List<Vector2Int> PositionVisited = new List<Vector2Int>();// Create the room at this position
    
    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonGenerationData)
    {
        List<DungeonClawer> dungeonClawers = new List<DungeonClawer>();
        for(int i = 0;i< dungeonGenerationData.numberOfCrawlers; i++)
        {
            dungeonClawers.Add(new DungeonClawer(Vector2Int.zero));
        }

        int iteration = Random.Range(dungeonGenerationData.iterationMin, dungeonGenerationData.iterationMax);

        for(int i = 0;i < iteration; i++)
        {
            foreach (DungeonClawer dungeonClawer in dungeonClawers)
            {
                Vector2Int newPosition = dungeonClawer.move(DirectionMovementMap);
                PositionVisited.Add(newPosition);
            }
        }
        return PositionVisited;
    }
}
