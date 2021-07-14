using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{
    public int GameLevel;
    public int GameScore;

    public List<GameLevelPassCondition> lvPassConditions;
    public GameObject ES;
    private EnemySpawn enemySpawn;
    enum PassOrNot
    {
        Pass,
        Not,
        Else,
    }


    // Start is called before the first frame update
    private void Awake()
    {
        GameLevel = 1;
        enemySpawn = ES.GetComponent<EnemySpawn>();
    }
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("BGM01");
        float BGM_Length = FindObjectOfType<SoundManager>().CheckLength("BGM01");
        Debug.Log(BGM_Length);
        StartCoroutine(PlayBGM("BGM01", BGM_Length));
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (checkPassGameLevel())
        {
            case (PassOrNot.Pass):
                GameLevel += 1;
                break;
            case (PassOrNot.Not):
                //Do not thing
                break;
            case (PassOrNot.Else):
                //Do not thing
                break;
        }

        
    }
    IEnumerator PlayBGM(string name,float DelayTime)
    {

        yield return new WaitForSeconds(DelayTime);
        FindObjectOfType<SoundManager>().Play("BGM01");
    }
    private PassOrNot checkPassGameLevel()
    {
        int currentGameLevel = GameLevel - 1;
        if ((lvPassConditions.Count - 1) >= currentGameLevel)
        {
            if (lvPassConditions[currentGameLevel].EnemyKilled == enemySpawn.Enemy1_Count + enemySpawn.Enemy2_Count)
            {
                return PassOrNot.Pass;
            }
            else
            {
                return PassOrNot.Not;
            }
        }
        else
        {
            return PassOrNot.Not;
        }
        //return PassOrNot.Else;

    }
    public void TestingHighScore()
    {
        GameScore = enemySpawn.GetEnemy_1_Count() + enemySpawn.GetEnemy_2_Count();
        if (PlayerPrefs.GetInt("highscore") < GameScore)
        {
            PlayerPrefs.SetInt("highscore", GameScore);
        }
    }
}
