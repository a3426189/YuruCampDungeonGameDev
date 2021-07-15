using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    //public DungeonClawerController clawerController;
    
    private List<Vector2Int> LocationOfDungeonRooms;
    private int IndexOfLocationOfDungeonRooms = 0;
    private void Start()
    {
        LocationOfDungeonRooms = DungeonClawerController.GenerateDungeon(dungeonGenerationData);
        //Debug.Log(LocationOfDungeonRooms.Count);
    }

    private void Update()
    {
        if (IndexOfLocationOfDungeonRooms < LocationOfDungeonRooms.Count)
            StartCoroutine(CreateDungeonRooms());
        else
        {
            StopCoroutine(CreateDungeonRooms());
            RoomController.instance.RemoveDoors();
        }
            
    }

    IEnumerator CreateDungeonRooms()
    {
        if (!RoomController.instance.isLoadingRoom)
        {
            RoomController.instance.CreateRoomData("Start", LocationOfDungeonRooms[IndexOfLocationOfDungeonRooms].x, LocationOfDungeonRooms[IndexOfLocationOfDungeonRooms].y);
            IndexOfLocationOfDungeonRooms++;
        }
        yield return null;
    }
}
