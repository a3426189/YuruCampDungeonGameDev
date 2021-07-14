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
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(RoomController.instance);
        if (RoomController.instance == null)
        {
            Debug.Log("You Pressed play at the wrong Scene");
            return;
        }
        else
        {
            //RoomController.instance.RegisterRoom(this);
        }
        RoomController.instance.RegisterRoom(this);

    }

    // Update is called once per frame
    void Update()
    {
        
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
            RoomController.instance.OnPlayerEnterRoom(this);
        }
        
    }
}
