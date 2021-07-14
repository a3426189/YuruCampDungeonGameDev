using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy : MonoBehaviour
{
    public GameObject prefab;
    public GameObject EnemyBullet;

    public float moveSpeed;
    private Rigidbody2D rb;
    private GameObject player;
    
    private HealBar healthbar;

    private Unit EnemyUnit;

    int Temp;
    private void Awake()
    {
        healthbar = prefab.GetComponent<HealBar>();
        EnemyUnit = gameObject.GetComponent<Unit>();
        player = GameObject.FindWithTag("player");
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        healthbar.SetMaxHealth(EnemyUnit.maxHP);
    }
    private void Update()
    {
        if (Temp != EnemyUnit.currentHP)
        {
            healthbar.SetHealth(EnemyUnit.currentHP);
        }
        OnEnable();
        Temp = EnemyUnit.currentHP;
        LookAtPlayer();
    }
    private void LookAtPlayer()
    {
        Vector2 PlayerVector2Pos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 EnemyVector2Pos = transform.position;
        Vector2 lookDir = PlayerVector2Pos - EnemyVector2Pos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("damage");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
        // rest of OnEnable
    }
}
