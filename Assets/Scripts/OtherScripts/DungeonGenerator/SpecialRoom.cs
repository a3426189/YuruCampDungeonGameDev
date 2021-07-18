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
    int randint;//for RandSpotToSpawn
    int NeedKilledEnemyCount = 1;
    int EnemyKilledNow = 1;

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
            randint =Random.Range(0, EnemySpawnList.Length);
            EnemySpawnList[randint].SetActive(true);
            NeedKilledEnemyCount = EnemySpawnList[randint].transform.childCount;
            EnemyKilledNow = GameManager.Instance.EnemyKilled;
            Debug.Log("進到危險房需擊殺人數" + NeedKilledEnemyCount);
        }
    }
    private bool OpenTheDoors()
    {
        if (NeedKilledEnemyCount == 0 || GameManager.Instance.EnemyKilled == 0) return false;
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
            this.gameObject.SetActive(false);
            return true;
        }
        return false;
        
        
     }
}
