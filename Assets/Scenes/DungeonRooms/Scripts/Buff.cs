using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject PlayerGo;
    private void Awake()
    {
        PlayerGo = GameObject.FindGameObjectWithTag("player");
        //PlayerUnit = PlayerGO.GetComponent<Unit>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //PlayerGo.GetComponent<Shooting>().GetBuff = true;
            Destroy(gameObject);
        }
            
    }
}
