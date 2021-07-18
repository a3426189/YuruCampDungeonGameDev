using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Special room only can have one,cuz opendoor bug. 
enum RoomCanBeSpawn
{
    //Special,
    Empty,
}

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    //public DungeonClawerController clawerController;
    private List<Vector2Int> LocationOfDungeonRooms;
    private int IndexOfLocationOfDungeonRooms = 0;

    bool GenerateisDone = false;
    bool GenerateEndRoom = false;
    

    private List<string> RoomNameList = new List<string>();//, "Special", "Special"

    private void Start()
    {
        RoomNameList.Add(RoomCanBeSpawn.Empty.ToString());
        //RoomNameList.Add(RoomCanBeSpawn.Special.ToString());


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
        if (IndexOfLocationOfDungeonRooms < LocationOfDungeonRooms.Count) { 
            StartCoroutine(CreateDungeonRooms());
        }
        else
        {
            //StopCoroutine(CreateDungeonRooms());
            if (!GenerateisDone && !RoomController.instance.isLoadingRoom)
            {
                GenerateisDone = true;
                CreateEndRoom();
                GenerateEndRoom = true;
            }
            GenerateMapAfterEndRoomIsGenerated();
        }
        
    }
    IEnumerator CreateDungeonRooms()
    {
        if (!RoomController.instance.isLoadingRoom)
        {
            int Randint = Random.Range(0, RoomNameList.Count);
            string roomName = RoomNameList[Randint];

            //if (roomName == RoomCanBeSpawn.Special.ToString())
            //{
            //    RoomNameList.Remove(RoomCanBeSpawn.Special.ToString());
            //}

            RoomController.instance.CreateRoomData(roomName, LocationOfDungeonRooms[IndexOfLocationOfDungeonRooms].x, LocationOfDungeonRooms[IndexOfLocationOfDungeonRooms].y);
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
    private void GenerateMapAfterEndRoomIsGenerated()
    {
        if (GenerateEndRoom == true && !RoomController.instance.isLoadingRoom)
        {
            RoomController.instance.RemoveDoors();

            foreach (Room room in RoomController.instance.Loaded_RoomList)
            {
                GameManager.Instance.Map.Add(new Vector2(room.X, room.Y));
            }
            GenerateEndRoom = !GenerateEndRoom;
        }
    }
}
