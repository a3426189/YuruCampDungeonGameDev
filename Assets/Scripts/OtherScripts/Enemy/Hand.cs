using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 EnemyPositionVector2 = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 PlayerPositionVector2 = new Vector2(player.transform.position.x, player.transform.position.y);
        float distance = Vector2.Distance(EnemyPositionVector2, PlayerPositionVector2);

        Vector2 lookDir = PlayerPositionVector2 - EnemyPositionVector2;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
