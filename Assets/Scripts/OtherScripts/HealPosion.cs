using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPosion : MonoBehaviour
{
    private GameObject PlayerGO;
    private Unit PlayerUnit;
    public int healamount;
    private void Awake()
    {
        PlayerGO = GameObject.FindGameObjectWithTag("player");
        PlayerUnit = PlayerGO.GetComponent<Unit>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {

            PlayerUnit.TakeHeal(healamount);
            Debug.Log(PlayerUnit.currentHP);
            Destroy(gameObject);

        }
        
    }
}
