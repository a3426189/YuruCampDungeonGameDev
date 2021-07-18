using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        int randint = Random.Range(0, gameObjects.Length);
        Instantiate(gameObjects[randint], this.gameObject.transform.position, Quaternion.identity);
    }
}
