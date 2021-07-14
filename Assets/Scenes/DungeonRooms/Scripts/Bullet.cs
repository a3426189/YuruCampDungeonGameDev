using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string ENEMY1 = "Enemy1";
    private string ENEMY2 = "Enemy2";
    private GameObject player;
    [SerializeField] private GameObject pfDamagePopUp;
    public GameObject EnemyPrefab_1;


    private float SCREEN_SIZE_X = 10f;
    private float SCREEN_SIZE_Y = 6f;

    private Unit PlayerUnit;
    private Unit EnemyUnit_1;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("player");
        PlayerUnit = player.GetComponent<Unit>();
        EnemyUnit_1 = EnemyPrefab_1.GetComponent<Unit>();
    }
    void Update()
    {
        if (rb.position.x > SCREEN_SIZE_X || rb.position.x < -SCREEN_SIZE_X || rb.position.y > SCREEN_SIZE_Y || rb.position.y < -SCREEN_SIZE_Y)
        {
            Destroy(gameObject);//outside of screen size.
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ENEMY2)
        {
            Unit EnemyUnit = collision.gameObject.GetComponent<Unit>();

            EnemyUnit.TakeDamage(PlayerUnit.damage);


            GameObject DamageText = Instantiate(pfDamagePopUp, transform.position, Quaternion.identity);
            DamagePopUp damagePopUp = DamageText.GetComponent<DamagePopUp>();
            damagePopUp.setup(PlayerUnit.damage);



            Destroy(gameObject);
            //Debug.Log("Hit!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ENEMY1)
        {
            Unit EnemyUnit = collision.gameObject.GetComponent<Unit>();
            
            EnemyUnit.TakeDamage(PlayerUnit.damage);


            GameObject DamageText = Instantiate(pfDamagePopUp, transform.position, Quaternion.identity);
            DamagePopUp damagePopUp = DamageText.GetComponent<DamagePopUp>();
            damagePopUp.setup(PlayerUnit.damage);
            Destroy(gameObject);
            //Debug.Log("Hit!");

        }
        
    }

}
