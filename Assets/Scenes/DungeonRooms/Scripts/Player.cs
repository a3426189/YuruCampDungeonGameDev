using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject EnemySpawn;


    private HashSet<GameObject> takenDamageFrom = new HashSet<GameObject>();
    public GameObject HealthBar;
    private HealBar healBar;
    public float speed;
    private Vector2 moveVelocity;
    
    private Unit PlayerUnit;

    private Vector2 mousepos;
    private Rigidbody2D rb;
    public Camera cam;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerUnit = GetComponent<Unit>();
        healBar = HealthBar.GetComponent<HealBar>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        healBar.SetMaxHealth(PlayerUnit.maxHP);
        healBar.SetHealth(PlayerUnit.currentHP);
    }
    // Update is called once per frame
    void Update()
    {
        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        healBar.SetHealth(PlayerUnit.currentHP);
        LookAtMouse();
        Inputs();
    }
    private void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime); //移動
        
    }
    private void LookAtMouse()
    {
        Vector2 lookDir = mousepos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        rb.rotation = angle;
    }
    private void Inputs()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If damager and not yet damaged (not contained in our references).
        if (collision.gameObject.tag == "damage" && !takenDamageFrom.Contains(collision.gameObject))
        {
            //Debug.Log("GET");
            PlayerUnit.TakeDamage(collision.gameObject.GetComponent<Unit>().damage);
            Debug.Log(PlayerUnit.currentHP);
            healBar.SetHealth(PlayerUnit.currentHP);
            // Mark as damaged.
            takenDamageFrom.Add(collision.gameObject);
            //takenDamageFrom.Remove(collision.gameObject);
            if (IsDead())
            {
                EnemySpawn enemySpawn = EnemySpawn.GetComponent<EnemySpawn>();
                Debug.Log(enemySpawn.Enemy1_Count + " " + enemySpawn.Enemy2_Count);

                takenDamageFrom.Clear();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (collision.gameObject.tag == "Enemy2")
        {
            Unit Enemy1Unit = collision.gameObject.GetComponent<Unit>();
            PlayerUnit.TakeDamage(Enemy1Unit.damage);
        }
    }
    private bool IsDead()
    {
        if (PlayerUnit.currentHP <= 0)
        {
            return true;
        }
        return false;
    }
}
