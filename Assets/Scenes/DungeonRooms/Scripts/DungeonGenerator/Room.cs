using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //public RoomController roomController;
    public int Width; 
    public int Height;
    public int X;
    public int Y;

    
    public Door Right_Door;
    public Door Left_Door;
    public Door Up_Door;
    public Door Down_Door;

    public List<Door> DoorList;
    // RoomController controller;
    // Start is called before the first frame update
    private void Awake()
    {
        RoomController.instance.RegisterRoom(this);
        
    }
    private void Start()
    {
        Door[] Ds = GetComponentsInChildren<Door>();
        foreach (Door door in Ds)
        {
            DoorList.Add(door);
            switch (door.doorType)
            {
                case Door.DoorType.up:
                    Up_Door = door;
                    break;
                case Door.DoorType.down:
                    Down_Door = door;
                    break;
                case Door.DoorType.right:
                    Right_Door = door;
                    break;
                case Door.DoorType.left:
                    Left_Door = door;
                    break;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in DoorList)
        {
            switch (door.doorType)
            {
                case Door.DoorType.up:
                    if (GetUpRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                        GameObject.Find("TopCloseDoor").SetActive(true);
                    }
                    break;
                case Door.DoorType.down:
                    if (GetDownRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                        GameObject.Find("BottomCloseDoor").SetActive(true);
                    }
                    break;
                case Door.DoorType.right:
                    if (GetRightRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                        GameObject.Find("RightCloseDoor").SetActive(true);
                    }
                    break;
                case Door.DoorType.left:
                    if (GetLeftRoom() == null)
                    {
                        door.gameObject.SetActive(false);
                        GameObject.Find("LeftCloseDoor").SetActive(true);
                    }
                    break;
            }
        }
    }

    public Room GetRightRoom()
    {
        if (RoomController.instance.DoesRoomExist(X + 1,Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetLeftRoom()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetUpRoom()
    {
        if (RoomController.instance.DoesRoomExist(X , Y + 1))
        {
            return RoomController.instance.FindRoom(X , Y + 1);
        }
        return null;
    }
    public Room GetDownRoom()
    {
        if (RoomController.instance.DoesRoomExist(X , Y + 1))
        {
            return RoomController.instance.FindRoom(X , Y + 1);
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }
    public Vector3 GetRoomCenter()//return a Vector3 world position
    {
        return new Vector3(X * Width, Y * Height);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            //controller.OnPlayerEnterRoom(this);
            RoomController.instance.OnPlayerEnterRoom(this);
        }
        
    }
}
