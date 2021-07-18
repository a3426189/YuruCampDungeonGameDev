using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitedMap : MonoBehaviour
{
    [SerializeField]
    private GameObject VisitedNote;
    [SerializeField]
    private GameObject SpaceNote;
    // Start is called before the first frame update
    List<Vector2> Current_PlayerVisited =new List<Vector2>();
    List<GameObject> VisitedNoteGameObjectList = new List<GameObject>();
    List<GameObject> SpaceNoteGameObjectList = new List<GameObject>();


    float Width = 18f;
    float Height = 10f;
    bool isdone = false;

    void Start()
    {
        //Current_PlayerVisited.Add(new Vector2(0, 0));
    }

    // Update is called once per frame
    void Update()
    {

        if (!isdone)
        {//generate map
            RoomChange();
            isdone = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            isdone = false;
            foreach (var item in SpaceNoteGameObjectList)
            {
                Destroy(item);
            }
            foreach (var item in VisitedNoteGameObjectList)
            {
                Destroy(item);
            }
            this.gameObject.SetActive(false);
        }
    }
    void RoomChange()
    {
        Debug.Log("MAP");
        
        //Current_PlayerVisited = New_PlayerVisited;
        SpaceNoteGameObjectList.Clear();
        VisitedNoteGameObjectList.Clear();

        Room Current_room = RoomController.instance.CurrentRoom;
        
        foreach (var item in GameManager.Instance.Map)
        {
            GameObject SpaceNoteGameObject = Instantiate(SpaceNote, new Vector3(item.x + (Current_room.X * Width), item.y + (Current_room.Y * Height), 0), Quaternion.identity);
            SpaceNoteGameObjectList.Add(SpaceNoteGameObject);
        }

        //foreach (var item in GameManager.Instance.PlayerVisited)
        //{
        //  GameObject VisitedNoteGameObject = Instantiate(VisitedNote, new Vector3(Item.X + (Current_room.X * Width), Item.Y + (Current_room.Y * Height), 0), Quaternion.identity);
        //  VisitedNoteGameObjectList.Add(VisitedNoteGameObject);
        //
        //}
        GameObject VisitedNoteGameObject = Instantiate(VisitedNote, new Vector3(Current_room.X + (Current_room.X * Width), Current_room.Y + (Current_room.Y * Height), 0), Quaternion.identity);
        VisitedNoteGameObjectList.Add(VisitedNoteGameObject);
    }
}
