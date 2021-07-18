using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Menu : MonoBehaviour
{
    public Animator MC1_animator;
    public Animator MC2_animator;
    
    public TextMeshProUGUI GamePhaseText;

    public TextMeshProUGUI GameLevelText;

    private void Start()
    {
        FindObjectOfType<SoundManager>().Play("BGM01");
        float BGM_Length = FindObjectOfType<SoundManager>().CheckLength("BGM01");
        StartCoroutine(PlayBGM("BGM01", BGM_Length));
    }
    private void Update()
    {
        GameLevelText.text = "YuruCamp Dungeon";
        if (PlayerPrefs.GetInt("Score") > 0)
        {
            GameLevelText.text = "ㄊ打倒了" + PlayerPrefs.GetInt("Score") +"個BOSS";
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("Score", 0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MakeTheSpriteShake()
    {
        //暫時
        GamePhaseText.text = "角色尚未開放";
        MC2_animator.Play("Shake");
        MC1_animator.Play("Idle");
    }
    public void Pick()
    {
        //暫時
        GamePhaseText.text = "Shima Rin";
        MC1_animator.Play("Pick");
        MC2_animator.Play("Idle");
    }
    IEnumerator PlayBGM(string name, float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        FindObjectOfType<SoundManager>().Play("BGM01");
    }
}
