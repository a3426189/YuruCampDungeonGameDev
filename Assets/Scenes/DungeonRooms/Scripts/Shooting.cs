using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public string ShootSound = "ShootSound";
    public Transform FirePos;
    
    //public Transform SpawnPos;
    public GameObject BulletPrefab;
    public float bullectDestoryTime;
    public float bulletForce;
    public float CanFirePreiod;
    
    bool CanFire = true;

    public bool IsUsing_Ult;
    float UltTime = 0;
    float UltTimeMax = 5f;

    public void Update()
    {
        if (IsUsing_Ult)
        {
            CanFirePreiod = 0.1f;
            UltTime += Time.deltaTime;
            if (UltTimeMax < UltTime)
            {
                UltTime = 0;
                IsUsing_Ult = false;
                CanFirePreiod = 0.5f;
            }
        }
        

        if (Input.GetButton("Fire1") && CanFire)
        {
            Shoot();
            CanFire = false;
            Invoke("canfire", CanFirePreiod);
        }
    }
    private void canfire()
    {
        CanFire = true;
    }
    private void Shoot()
    {
        FindObjectOfType<SoundManager>().Play(ShootSound);
        GameObject bullet = Instantiate(BulletPrefab, FirePos.position, FirePos.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-FirePos.right * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, bullectDestoryTime);

    }
}
