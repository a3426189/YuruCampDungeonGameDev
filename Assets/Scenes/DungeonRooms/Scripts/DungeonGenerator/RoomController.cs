using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * CreateRoomData ->  DoesRoomExist -> UpdateRoomDataQueue -> StartCoroutine(LoadScene(room)) -> register room;
 * 說明:
 * 一開始先建立RoomData class 資料有 name,X,Y ，{X,Y}代表該Room的虛擬位址，之後需要紀錄在Roomlist中避免在DeQueue後消失，
 * 再來通過CreateRoomData新建RoomData物件，且再建立前須查看Roomlist中是否已經有生成過相同位址的Room，避免重複生成在同個位置上，
 * ，確認完後存放在RoomDataQueue，可RoomDataQueue.DeQueue拿到CurrentRoomData，之後便可開始載入Scene，因為載入的場景中有我們事先準備好的Room
 * 因此會觸發到Room中Start中的函式:RegisterRoom，以此紀錄CurrentRoomData中的資訊，避免之後的DeQueue後資料消失。
 */


public class RoomData
{
    public string name;
    public int X;
    public int Y;

    public RoomData(string name,int X,int Y)
    {
        this.name = name;
        this.X = X;
        this.Y = Y;
    }
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string Current_WorldName = "basement";
    Room CurrentRoom;
    RoomData Current_RoomData;
    //Current_LoadRoomData.name;
    Queue<RoomData> RoomDataQueue = new Queue<RoomData>();

    public List<Room> Loaded_RoomList = new List<Room>();

    bool isLoadingRoom = false;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateRoomData("Start", 0, 0);
        CreateRoomData("Start", 1, 0);
    }

    private void Update()
    {
        UpdateRoomQueue();
    }
    private void UpdateRoomQueue()
    {
       if (isLoadingRoom)
        {
            return;
        }
       if (RoomDataQueue.Count == 0)
        {
            return;
        }
        Current_RoomData = RoomDataQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(Current_RoomData));
    }
    public void CreateRoomData(string name, int x ,int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }
        RoomData NewRoomData = new RoomData(name,x,y);
        RoomDataQueue.Enqueue(NewRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomData roomData)
    {
        string RoomName = Current_WorldName + roomData.name;//SceneName
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(RoomName,LoadSceneMode.Additive);
        while(!loadRoom.isDone)
        {
            yield return null;
        }
    }
    public void RegisterRoom(Room room)
    {
        
        room.transform.position = new Vector3(
            Current_RoomData.X * room.Width,
            Current_RoomData.Y * room.Height,
            0
        );
        room.X = Current_RoomData.X;
        room.Y = Current_RoomData.Y;
        room.name = Current_WorldName + "-" + Current_RoomData.name + " {" + room.X +","+ room.Y +"}";
        //room.transform.parent = transform;//?

        if (Loaded_RoomList.Count == 0)//起始點
        {
            CameraController.Instance.currRoom = room;
        }

        isLoadingRoom = false;
        Loaded_RoomList.Add(room);
    }


    public bool DoesRoomExist(int x , int y)
    {

        if (Loaded_RoomList.Find(Room => Room.X == x && Room.Y == y) != null)
        {
            return true;
        }
        return false;
        
    }

    public void OnPlayerEnterRoom(Room room)
    {
        
        CurrentRoom = room;
        CameraController.Instance.currRoom = room;
    }
    
}
