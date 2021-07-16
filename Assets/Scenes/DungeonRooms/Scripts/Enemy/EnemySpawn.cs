using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Difficulty
{
    easy,
    normal,
    hard,
    impossible,
    //endless,
}
public class EnemySpawn : MonoBehaviour
{
    public GameObject[] objects;

    public List<GameObject> Enemy_gameObjects;
    public List<GameObject> Spawned_Enemys;
    public int Enemy1_Count;
    public int Enemy2_Count;
    public GameObject player;

    public float minDistance = 1.5f;
    
    Difficulty difficulty;
    private float repeatSpawnTime = 1.5f;
    private float Spawntime = 0f;
    private float time;

    private float NextDiffTime = 20f;


    private int EnemyAmount;
    private Unit PlayerUnit;
    private List<GameObject> EnemyGameObjectList = new List<GameObject>();
    private bool too_close = false;

    [SerializeField]
    private GameObject pfHealPosion;
    [SerializeField]
    private GameObject pfBuff;
    // Start is called before the first frame update
    private void Start()
    {
        Spawn();
        //player = GameObject.FindWithTag("player");
        //PlayerUnit = player.GetComponent<Unit>();

        //difficulty = Difficulty.easy;
        //SetDifficulty();
        
    }

    // Update is called once per frame

    
    private void Spawn()
    {
        //too_close = false;
        int  RandomInt= Random.Range(0, objects.Length);
        Instantiate(objects[RandomInt],transform.position,Quaternion.identity);

        //GameObject EnemyGO = Instantiate(EnemyGameObjectList[RandomInt], position, Quaternion.identity); ;
        


    }
    private bool CheckGameObjectIsAlive(GameObject GO)
    {
        Unit unit = GO.GetComponent<Unit>();
        Animator animator = GO.GetComponent<Animator>();
        //Debug.Log(unit.currentHP);
        if (unit.currentHP <= 0)
        {
            if (unit.unitName == "Nadeshiko")
            {
                Enemy1_Count += 1;
                animator.Play("Death");
                FindObjectOfType<SoundManager>().Play("Enemy1DeathSound");
                
                //Debug.Log(unit.unitName);
            }
            if (unit.unitName == "Chiaki")
            {
                Enemy2_Count += 1;
                animator.Play("Death");
                FindObjectOfType<SoundManager>().Play("Enemy1DeathSound");
                //Debug.Log(unit.unitName);
            }
            

            DropItem(GO);
            
            EnemyGameObjectList.Remove(GO);
            float timeToDestory = 0.5f;
            StartCoroutine(DestroyGO(GO,timeToDestory));
            
            return false;
        }
        return true;
    }
    IEnumerator DestroyGO(GameObject GO,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(GO);
    }
    private void SetDifficulty()
    {
        switch (difficulty)
        {
            case (Difficulty.easy):
                repeatSpawnTime = 1.5f;
                EnemyAmount = 3;
                if (time >= NextDiffTime)
                {
                    time -= NextDiffTime;
                    difficulty = Difficulty.normal;
                    Debug.Log(difficulty + " " + EnemyAmount);
                }
                
                break;
            case (Difficulty.normal):
                repeatSpawnTime = 1.2f;
                EnemyAmount = 4;

                if (time >= NextDiffTime)
                {
                    time -= NextDiffTime;
                    difficulty = Difficulty.hard;
                    Debug.Log(difficulty + " " + EnemyAmount);
                }
                break;
            case (Difficulty.hard):
                repeatSpawnTime = 1f;
                EnemyAmount = 6;
                if (time >= NextDiffTime)
                {
                    time -= NextDiffTime;
                    difficulty = Difficulty.impossible;
                    Debug.Log(difficulty + " " + EnemyAmount);
                }
                break;
            case (Difficulty.impossible):
                repeatSpawnTime = 0.5f;
                EnemyAmount = 8;
                if (time >= NextDiffTime)
                {
                    time -= NextDiffTime;
                    //difficulty = Difficulty.endless;
                }
                break;
            /*
            case (Difficulty.endless):
                if (repeatSpawnTime > 0.2f) repeatSpawnTime -=0.1f;
                if (EnemyAmount < 10) EnemyAmount += 1;
                break;
            */
        }
    }
    private void DropItem(GameObject GO)
    {
        int RandInt = Random.Range(1, 6);// 1~5
        switch (RandInt)
        {
            case (1):
                Instantiate(pfHealPosion, GO.transform.position, Quaternion.identity);
                break;
            case (2):
                Instantiate(pfHealPosion, GO.transform.position, Quaternion.identity);
                break;
            case (3):
                Instantiate(pfBuff, GO.transform.position, Quaternion.identity);
                break;
            case (4):

                break;
            case (5):

                break;
        }
    }
    public int GetEnemy_1_Count()
    {
        return Enemy1_Count;
    }
    public int GetEnemy_2_Count()
    {
        return Enemy2_Count;
    }
}
