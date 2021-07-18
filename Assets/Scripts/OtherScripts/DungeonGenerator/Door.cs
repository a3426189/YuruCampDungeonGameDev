using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
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

    public BoxCollider2D BoxCollider2D;
    private void Start()
    {
        
        BoxCollider2D.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            BoxCollider2D.isTrigger = false;
        }
        if (collision.gameObject.tag == "player")
        {
            BoxCollider2D.isTrigger = true;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            BoxCollider2D.isTrigger = false;
        }
        if (collision.gameObject.tag == "player")
        {
            BoxCollider2D.isTrigger = true;
        }
    }
}
