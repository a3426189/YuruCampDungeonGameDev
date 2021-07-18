using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public Room currRoom;
    public float moveSpeedWhenRoomChange;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsSwitchingScene())
        {
            UpdataCameraPosition();
        }
    }

    void UpdataCameraPosition()
    {
        if (currRoom == null)
        {
            return;
        }
        Vector3 targetPos = GetCameraTargetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);
    }
    Vector3 GetCameraTargetPosition()
    {
        if (currRoom == null)
        {
            return Vector3.zero;
        }
        Vector3 targetPos = currRoom.GetRoomCenter();
        targetPos.z = transform.position.z;
        return targetPos;
    }
    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
