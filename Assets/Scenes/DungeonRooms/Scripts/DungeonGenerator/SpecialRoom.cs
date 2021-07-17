using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoom : MonoBehaviour
{
    public GameObject OpenDoorUp;
    public GameObject OpenDoorLeft;
    public GameObject OpenDoorRight;
    public GameObject OpenDoorDown;

    public GameObject CloseDoorUp;
    public GameObject CloseDoorLeft;
    public GameObject CloseDoorRight;
    public GameObject CloseDoorDown;

    public GameObject[] EnemySpawnList;

    bool FirstTime = true;

    int NeedKilledEnemyCount;
    int EnemyKilledNow = 0;
    private void Start()
    {
        
    }
    private void Update()
    {
        OpenTheDoors();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag =="player" || collision.gameObject.tag == "Player") && FirstTime)
        {
            FirstTime = false;
            if (RoomController.instance.CurrentRoom.GetUpRoom() != null)
            {
                OpenDoorUp.SetActive(false);
                CloseDoorUp.SetActive(true);
            }
            if (RoomController.instance.CurrentRoom.GetLeftRoom() != null)
            {
                OpenDoorLeft.SetActive(false);
                CloseDoorLeft.SetActive(true);
            }
            if (RoomController.instance.CurrentRoom.GetRightRoom() != null)
            {
                OpenDoorRight.SetActive(false);
                CloseDoorRight.SetActive(true);
            }
            if (RoomController.instance.CurrentRoom.GetDownRoom() != null)
            {
                OpenDoorDown.SetActive(false);
                CloseDoorDown.SetActive(true);
            }
            int randint =Random.Range(0, EnemySpawnList.Length);
            EnemySpawnList[randint].SetActive(true);
            NeedKilledEnemyCount = EnemySpawnList[randint].transform.childCount;
            EnemyKilledNow = GameManager.Instance.EnemyKilled;
        }
    }
    private void OpenTheDoors()
    {
        if (GameManager.Instance.EnemyKilled == (EnemyKilledNow + NeedKilledEnemyCount))
        {
            if (RoomController.instance.CurrentRoom.GetUpRoom() != null)
            {
                OpenDoorUp.SetActive(true);
                CloseDoorUp.SetActive(false);
            }
            if (RoomController.instance.CurrentRoom.GetLeftRoom() != null)
            {
                OpenDoorLeft.SetActive(true);
                CloseDoorLeft.SetActive(false);
            }
            if (RoomController.instance.CurrentRoom.GetRightRoom() != null)
            {
                OpenDoorRight.SetActive(true);
                CloseDoorRight.SetActive(false);
            }
            if (RoomController.instance.CurrentRoom.GetDownRoom() != null)
            {
                OpenDoorDown.SetActive(true);
                CloseDoorDown.SetActive(false);
            }
        }
    }
}
