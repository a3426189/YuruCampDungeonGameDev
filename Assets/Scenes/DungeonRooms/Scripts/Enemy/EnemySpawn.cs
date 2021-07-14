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
    public int Enemy1_Count;
    public int Enemy2_Count;


    public GameObject player;
    public GameObject EnemyPrefab_1;
    public GameObject EnemyPrefab_2;
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
        player = GameObject.FindWithTag("player");
        PlayerUnit = player.GetComponent<Unit>();

        difficulty = Difficulty.easy;
        SetDifficulty();
        
    }

    // Update is called once per frame

    private void Update()
    {
        //Debug.Log(EnemyGameObjectList.Count);
        if (EnemyGameObjectList.Count > 0)
        {
            for (int i = 0; i < EnemyGameObjectList.Count; i++)
            {
                GameObject GO = EnemyGameObjectList[i];
                CheckGameObjectIsAlive(GO);
            }
        }
        time += Time.deltaTime;
        Spawntime += Time.deltaTime;
        
        SetDifficulty();

        if (Spawntime >= repeatSpawnTime)
        {
            Spawn();
            Spawntime -= repeatSpawnTime;
        }

        
        

    }
    private void Spawn()
    {
        too_close = false;
        int SpawnEnemyKinds = Random.Range(1, 4);
        GameObject EnemyGO;
        switch (SpawnEnemyKinds)
        {
            case (1):
                EnemyGO = EnemyPrefab_1;
                break;
            case (2):
                EnemyGO = EnemyPrefab_2;
                break;
            default:
                EnemyGO = EnemyPrefab_1;
                break;
        }
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //Vector3 position = new Vector3(Random.Range(-8.0f, 6.0f), Random.Range(-4f, 4f), 0);
        Vector3 position = new Vector3(Random.Range(minScreenBounds.x, maxScreenBounds.x), Random.Range(minScreenBounds.y, maxScreenBounds.y), 0);
        if (Vector3.Distance(player.transform.position, EnemyGO.transform.position) < minDistance)
            too_close = true;
        
        if (EnemyGameObjectList.Count > 0)
        {
            for(int i = 0;i< EnemyGameObjectList.Count; i++)
            {
                GameObject GO = EnemyGameObjectList[i];
                if ((Vector3.Distance(GO.transform.position, position) < minDistance))
                {
                    too_close = true;
                }
            }
        }
        if (!too_close && (EnemyGameObjectList.Count < EnemyAmount))
        {
            GameObject EnemyGameObject = Instantiate(EnemyGO, position, Quaternion.identity);
            EnemyGameObjectList.Add(EnemyGameObject);
        }
        
        
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
