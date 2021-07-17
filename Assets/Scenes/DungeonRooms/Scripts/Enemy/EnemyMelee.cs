using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    GameObject player;
    Vector2 EnemyPositionVector2;
    Vector2 PlayerPositionVector2;
    public float TooCloseDistance;
    string currentAnimaton;
    //public Animator animator;
    Animator animator;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        player = GameObject.FindWithTag("player");
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyPositionVector2 = new Vector2(this.transform.position.x, this.transform.position.y);
        PlayerPositionVector2 = new Vector2(player.transform.position.x, player.transform.position.y);
        float distance = Vector2.Distance(EnemyPositionVector2, PlayerPositionVector2);

        if (distance < TooCloseDistance)
        {
            ChangeAnimationState("EnemyMeleeAttack");
            //FindObjectOfType<SoundManager>().Play("Enemy1DeathSound");
        }
        else
        {
            if (makeADelayToDoSomething(1f))
            {
                ChangeAnimationState("Idle");
            }
        }
    }
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimaton))
        //{
        //    return;
        //}
        currentAnimaton = newAnimation;
        animator.Play(currentAnimaton);
    }
    bool makeADelayToDoSomething(float TimeMax)
    {
        time += Time.deltaTime;
        if(time > TimeMax)
        {
            time = 0;
            return true;
        }
        return false;
    }

}
