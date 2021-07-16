using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door:MonoBehaviour
{
    public enum DoorType
    {
        up,
        right,
        left,
        down
    }
    public DoorType doorType;
    public GameObject CloseDoor;
    
}
