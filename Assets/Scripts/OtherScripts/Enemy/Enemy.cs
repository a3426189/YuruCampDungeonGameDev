using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public GameObject HealBar;
    public Room room;
    public float moveSpeed;
    private Rigidbody2D rb;

    [SerializeField]
    private Rigidbody2D Handrb;
    [SerializeField]
    private GameObject pfHealPosion;
    [SerializeField]
    private GameObject pfBuff;

    private GameObject player;
    float CurrentHP;

    HealBar healthbar;

    private Unit EnemyUnit;
    private Component component;
    private bool GetKilled = false;
    int Temp;
    private void Awake()
    {
        healthbar = HealBar.GetComponent<HealBar>();
        EnemyUnit = gameObject.GetComponent<Unit>();
        player = GameObject.FindWithTag("player");
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        room = RoomController.instance.CurrentRoom;
        //EnemyUnit.currentHP = EnemyUnit.currentHP * EnemyUnit.unitLevel;
        //EnemyUnit.damage = EnemyUnit.damage * EnemyUnit.unitLevel;
        healthbar.SetMaxHealth(EnemyUnit.maxHP);
    }
    private void Update()
    {
        
        if (!GetKilled)
        {
            GetKilled = IsDead();
        }
        
        
        //OnEnable();
        if(room == RoomController.instance.CurrentRoom)
        {
            LookAtPlayer();
            //rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            //rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
            //transform.position = transform.position;
        }
    }
    
    private void LookAtPlayer()
    {
        Vector2 PlayerVector2Pos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 EnemyVector2Pos = transform.position;
        Vector2 lookDir = PlayerVector2Pos - EnemyVector2Pos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        if(Handrb!=null) Handrb.rotation = (angle - 90);


        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    private bool IsDead()
    {
        if(EnemyUnit.currentHP <= 0)
        {
            DropItem();
            //Debug.Log(EnemyUnit.currentHP);
            Animator animator = this.GetComponent<Animator>();
            if(animator!=null) animator.Play("Death");
            FindObjectOfType<SoundManager>().Play("Enemy1DeathSound");
            GameManager.Instance.EnemyKilled++;
            
            Destroy(gameObject, 0.5f);
            return true;
        }
        return false;
    }
    
    private void DropItem()
    {
        int RandInt = Random.Range(1, 6);// 1~5
        switch (RandInt)
        {
            case (1):
                Instantiate(pfHealPosion, this.transform.position, Quaternion.identity);
                break;
            case (2):
                Instantiate(pfHealPosion, this.transform.position, Quaternion.identity);
                break;
            case (3):
                Instantiate(pfBuff, this.transform.position, Quaternion.identity);
                break;
            case (4):

                break;
            case (5):

                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            EnemyUnit.TakeDamage(2);
            healthbar.SetHealth(EnemyUnit.currentHP);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            EnemyUnit.TakeDamage(2);
            healthbar.SetHealth(EnemyUnit.currentHP);
        }
    }
}
