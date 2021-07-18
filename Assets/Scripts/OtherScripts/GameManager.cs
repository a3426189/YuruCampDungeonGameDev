using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public int EnemyKilled;
    public List<Vector2> PlayerVisited;
    public List<Vector2> Map;
    

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject VisitedMap;

    public int GameLevel;
    private int CurrentGameLevel;
    public int GamingTime;
    public int GameScore;

    private bool IsFightingBoss;
    
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
        IsFightingBoss = false;
        FindObjectOfType<SoundManager>().Play("BGM01");
        float BGM_Length = FindObjectOfType<SoundManager>().CheckLength("BGM01");
        //Debug.Log(BGM_Length);
        StartCoroutine(PlayBGM("BGM01", BGM_Length));
        GameLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerState();//can be use for testing
        FightBoss(GameObject.FindGameObjectWithTag("Boss"));
        
        if (Input.GetKey(KeyCode.Tab))
        {
            VisitedMap.SetActive(true);
        }
        
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
    private void PlayerState()
    {
        Unit playerUnit = player.GetComponent<Unit>();
        if (playerUnit.currentHP <=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    private void FightBoss(GameObject Boss)
    {
        if (Boss!=null)
        {
            IsFightingBoss = true;
        }
        if (IsFightingBoss)
        {
            if(GameObject.FindGameObjectWithTag("Boss") == null)
            {
                GameLevel++;
                player.GetComponent<Unit>().damage *= 0.1f;
                IsFightingBoss = false;
                LevelChange(GameLevel);
            }
        }
    }
    private void LevelChange(int newLevel)
    {
        PlayerPrefs.SetInt("Score", newLevel - 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Go next Level
        //Beat the Boss -> GameLevel += 1;
    }
}
