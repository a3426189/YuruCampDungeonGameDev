using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string ENEMY = "Enemy";
    private GameObject player;
    [SerializeField] private GameObject pfDamagePopUp;
    private Unit PlayerUnit;
    private Unit EnemyUnit_1;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("player");
        PlayerUnit = player.GetComponent<Unit>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == ENEMY)
        {
            Unit EnemyUnit = collision.gameObject.GetComponent<Unit>();
            //showDamage();
            EnemyUnit.TakeDamage(PlayerUnit.damage);
            Destroy(gameObject);
            //Debug.Log("Hit!");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ENEMY)
        {
            Unit EnemyUnit = collision.gameObject.GetComponent<Unit>();
            //showDamage();
            EnemyUnit.TakeDamage(PlayerUnit.damage);
            Destroy(gameObject);
        }
    }
    private void showDamage()
    {
        GameObject DamageText = Instantiate(pfDamagePopUp, transform.position, Quaternion.identity);
        DamagePopUp damagePopUp = DamageText.GetComponent<DamagePopUp>();
        damagePopUp.setup(PlayerUnit.damage);
    }

}
