using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    public Transform FirePos;
    //public Transform SpawnPos;
    public GameObject Bullet;
    public float bullectDestoryTime = 2f;
    public float bulletForce = 15f;
    [SerializeField]
    private float timeMax;

    private float Store_timeMax;

    [SerializeField]
    private float BuffTimeMax;

    private float BuffTime;
    public bool GetBuff;

    private bool CanFire = true;
    private void Awake()
    {
        Store_timeMax = timeMax;
    }
    public void Update()
    {
        if (GetBuff == false)
        {
            BuffTime += Time.deltaTime;
            if (BuffTime > BuffTimeMax)
            {
                BuffTime -= BuffTimeMax;
                timeMax = Store_timeMax;
            }
        }
        
        if (GetBuff)
        {
            GetBuff = !GetBuff;
            
            timeMax = 0.1f;
            BuffTime = 0;
            
        }
        if (Input.GetButton("Fire1") && CanFire)
        {
            Shoot();

            CanFire = false;
            Invoke("canfire", timeMax);
        }
    }
    private void DisableBuff()
    {
        GetBuff = false;
    }
    private void canfire()
    {
        CanFire = true;
    }
    private void Shoot()
    {
        string ShootSound = "ShootSound";
        FindObjectOfType<SoundManager>().Play(ShootSound);
        //GameObject Decorate = Instantiate(Bullet, SpawnPos.position, SpawnPos.rotation);
        GameObject bullet = Instantiate(Bullet, FirePos.position, FirePos.rotation);
        Destroy(bullet, bullectDestoryTime);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-FirePos.right * bulletForce, ForceMode2D.Impulse);
        
    }
}
