using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public GameObject EnemySpawn;
    private HashSet<GameObject> takenDamageFrom = new HashSet<GameObject>();

    public GameObject HealthBar;
    private HealBar healBar;

    public float speed;
                
    private Vector2 moveVelocity;
    
    private Unit PlayerUnit;
    private Vector2 mousepos;

    private Rigidbody2D rb2D;

    public Camera cam;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        PlayerUnit = GetComponent<Unit>();
        healBar = HealthBar.GetComponent<HealBar>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        healBar.SetMaxHealth(PlayerUnit.maxHP);
        healBar.SetHealth(PlayerUnit.currentHP);
    }
    // Update is called once per frame
    void Update()
    {
        healBar.SetHealth(PlayerUnit.currentHP);
        LookAtMouse();
        Inputs();
    }
    
    private void LookAtMouse()
    {
        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousepos - rb2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        rb2D.rotation = angle;
    }
    private void Inputs()
    {
        float MoveH = Input.GetAxis("Horizontal");
        float MoveV = Input.GetAxis("Vertical");
        rb2D.velocity = new Vector2(MoveH * speed, MoveV * speed);
    }
    private bool IsDead()
    {
        if (PlayerUnit.currentHP <= 0)
        {
            return true;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "damage")
        {
            PlayerUnit.TakeDamage(1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "damage")
        {
            PlayerUnit.TakeDamage(1);
        }
    }
}
