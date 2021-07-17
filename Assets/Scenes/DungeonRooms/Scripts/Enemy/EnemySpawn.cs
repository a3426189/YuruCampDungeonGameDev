using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] objects;

    private void Start()
    {
        Spawn();
        //SetDifficulty();
        
    }
    private void Spawn()
    {
        int  RandomInt= Random.Range(0, objects.Length);
        Instantiate(objects[RandomInt],transform.position,Quaternion.identity);
    }
    
}
