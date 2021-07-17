using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    HealBar healBar;
    Unit PlayerUnit;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player" || collision.gameObject.tag == "Player")
        {
            PlayerUnit = collision.gameObject.GetComponent<Unit>();
            //PlayerUnit.TakeDamage(1);
            Destroy(gameObject);
        }
        
    }
    
}
