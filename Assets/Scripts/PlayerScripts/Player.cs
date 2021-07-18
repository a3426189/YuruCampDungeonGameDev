using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public static Player Instance;

    public GameObject HealthBar;
    private HealBar healBar;

    public float speed;
                
    private Vector2 moveVelocity;
    
    private Unit PlayerUnit;
    private Vector2 mousepos;

    Animator animator;
    string CurrentAnimation;

    string PlayerGetDamagedAnimation = "Player GetDamaged";
    string PlayerIdle = "Idle";

    bool Invincible_Active = false;
    float Invincible_Time = 0;
    float Invincible_MAX = 2f;
    public Camera cam;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        animator = this.GetComponent<Animator>();
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
        Invincible();//被打會有短時間的無敵 也許之後可以做成能力?
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "damage" && !Invincible_Active)
        {
            PlayerUnit.TakeDamage(1);
            ChangeAnimationState(PlayerGetDamagedAnimation);
            FindObjectOfType<SoundManager>().Play("Enemy1DeathSound");
            Invincible_Active = true;
        }
    }
    private void Invincible()
    {
        if (Invincible_Active)
        {
            Invincible_Time += Time.deltaTime;
            if (Invincible_Time > Invincible_MAX)
            {
                Invincible_Time = 0;
                Invincible_Active = false;
                ChangeAnimationState(PlayerIdle);
            }
        }
    }
    private void ChangeAnimationState(string newAnimation) 
    {
        if (newAnimation == CurrentAnimation) return;
        animator.Play(newAnimation);
    }
}
