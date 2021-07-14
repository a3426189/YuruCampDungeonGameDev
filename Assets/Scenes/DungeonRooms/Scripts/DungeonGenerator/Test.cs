using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomData1
{
    public string name;
    public int X;
    public int Y;

    public RoomData1(string name,int x ,int y)
    {
        this.name = name;
        this.X = x;
        this.Y = y;
    }
}




public class Test : MonoBehaviour
{
    public static Test Instance;
    private void Awake()
    {
        Instance = this;
    }

    RoomData1 NewRoomData;
    
    Queue<RoomData1> RoomData_Queue = new Queue<RoomData1>();
    
    List<Room> RoomList = new List<Room>();
    string worldname = "basement";
    bool Isloading = false;

    
    private void Start()
    {
        CreateNewRoom("",0,0);
    }
    private void Update()
    {
        RoomData_QueueUpdate();
    }
    private void RoomData_QueueUpdate()
    {
        if (Isloading)
        {
            return;
        }
        if (RoomData_Queue.Count == 0)
        {
            return;
        }

        RoomData1 roomData1 = RoomData_Queue.Dequeue();

        StartCoroutine(LoadRoomSceneCoroutine(roomData1));

        Isloading = true;

    }
    private void CreateNewRoom(string name,int x,int y)
    {
        if (DoesRoomExists(x,y))//不重複
        {
            return;
        }

        NewRoomData = new RoomData1(name, x, y);
        RoomData_Queue.Enqueue(NewRoomData);
    }
    

    IEnumerator LoadRoomSceneCoroutine(RoomData1 roomData)
    {
        string SceneName = worldname + roomData.name;
        AsyncOperation LoadRoomAsync = SceneManager.LoadSceneAsync(SceneName,LoadSceneMode.Additive);//讓多個Scene在同個Scene中
        if (!LoadRoomAsync.isDone)
        {
            yield return null;
        }
   }
    private bool DoesRoomExists(int x, int y)
    {
        return RoomList.Find(room => room.X == x && room.Y == y) != null;
    }
}
