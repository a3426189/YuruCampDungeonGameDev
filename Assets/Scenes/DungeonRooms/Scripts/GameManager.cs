using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public int EnemyKilled;
    public List<Vector2> PlayerVisited;
    public int GameLevel;
    public int GameScore;

    //public List<GameLevelPassCondition> lvPassConditions;
    public GameObject ES;

    enum PassOrNot
    {
        Pass,
        Not,
        Else,
    }
    

    // Start is called before the first frame update
    
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("BGM01");
        float BGM_Length = FindObjectOfType<SoundManager>().CheckLength("BGM01");
        //Debug.Log(BGM_Length);
        StartCoroutine(PlayBGM("BGM01", BGM_Length));
        
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }
    IEnumerator PlayBGM(string name,float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        FindObjectOfType<SoundManager>().Play("BGM01");
    }
    public void TestingHighScore()
    {
        GameScore = EnemyKilled;
        if (PlayerPrefs.GetInt("highscore") < GameScore)
        {
            PlayerPrefs.SetInt("highscore", GameScore);
        }
    }

}
