using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : MonoBehaviour
{
    
    public Transform FirePos;
    //public Transform SpawnPos;
    public GameObject Bullet;
    public float bullectDestoryTime = 6f;
    public float bulletForce = 3f;

    private float ShootTime;
    public float ShootTimeMax;
    public Room room;

    private void Start()
    {
        room = RoomController.instance.CurrentRoom;

        //InvokeRepeating("Shoot", 1f, 2f);
    }
    private void Update()
    {
        
        if (room == RoomController.instance.CurrentRoom)
        {
            if (ReadyShoot())
            {
                Shoot();

                //Shoot();
            }
        }
    }
    private bool ReadyShoot()
    {
        ShootTime += Time.deltaTime;
        if (ShootTime >= ShootTimeMax)
        {
            ShootTime = 0;
            return true;
        }
        return false;
    }
    private void Shoot()
    {
        //GameObject Decorate = Instantiate(Bullet, SpawnPos.position, SpawnPos.rotation);
        GameObject bullet = Instantiate(Bullet, FirePos.position, FirePos.rotation);
        Destroy(bullet, bullectDestoryTime);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePos.right * bulletForce, ForceMode2D.Impulse);
        
    }
}
