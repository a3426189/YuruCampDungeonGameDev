using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    //public DungeonClawerController clawerController;
    private List<Vector2Int> LocationOfDungeonRooms;
    private int IndexOfLocationOfDungeonRooms = 0;

    bool isDone = false;

    private string[] RoomNameArray = { };

    private void Start()
    {
        LocationOfDungeonRooms = DungeonClawerController.GenerateDungeon(dungeonGenerationData);
        foreach (var item in LocationOfDungeonRooms)
        {
            //Debug.Log(item);
        }
        RoomController.instance.CreateRoomData("Start", 0, 0);

        //Debug.Log(LocationOfDungeonRooms.Count);
    }

    private void Update()
    {
        if (IndexOfLocationOfDungeonRooms < LocationOfDungeonRooms.Count)
            StartCoroutine(CreateDungeonRooms());
        else
        {
            //StopCoroutine(CreateDungeonRooms());
            if (!isDone && !RoomController.instance.isLoadingRoom)
            {
                isDone = true;
                CreateEndRoom();
            }
            
        }
        if (isDone == true && !RoomController.instance.isLoadingRoom)
        {
            //Debug.Log(RoomController.instance.Loaded_RoomList.Count);
            RoomController.instance.RemoveDoors();
        }
    }
    IEnumerator CreateDungeonRooms()
    {
        if (!RoomController.instance.isLoadingRoom)
        {
            RoomController.instance.CreateRoomData("Empty", LocationOfDungeonRooms[IndexOfLocationOfDungeonRooms].x, LocationOfDungeonRooms[IndexOfLocationOfDungeonRooms].y);
            IndexOfLocationOfDungeonRooms++;
        }
        yield return null;
    }

    private void CreateEndRoom()
    {
        List<Room> Rooms = RoomController.instance.Loaded_RoomList;

        if (RoomController.instance.FindRoom(Rooms[Rooms.Count - 1].X + 1, Rooms[Rooms.Count - 1].Y) == null)//右邊無房子
        {
            RoomController.instance.CreateRoomData("End", Rooms[Rooms.Count - 1].X + 1, Rooms[Rooms.Count - 1].Y);
            return;
        }
        if (RoomController.instance.FindRoom(Rooms[Rooms.Count - 1].X - 1, Rooms[Rooms.Count - 1].Y) == null)//左邊無房子
        {
            RoomController.instance.CreateRoomData("End", Rooms[Rooms.Count - 1].X - 1, Rooms[Rooms.Count - 1].Y);
            return;
        }
        if (RoomController.instance.FindRoom(Rooms[Rooms.Count - 1].X, Rooms[Rooms.Count - 1].Y +1) == null)//上邊無房子
        {
            RoomController.instance.CreateRoomData("End", Rooms[Rooms.Count - 1].X , Rooms[Rooms.Count - 1].Y +1);
            return;
        }
        if (RoomController.instance.FindRoom(Rooms[Rooms.Count - 1].X, Rooms[Rooms.Count - 1].Y -1) == null)//下邊無房子
        {
            RoomController.instance.CreateRoomData("End", Rooms[Rooms.Count - 1].X , Rooms[Rooms.Count - 1].Y -1);
            return;
        }


    }
}
