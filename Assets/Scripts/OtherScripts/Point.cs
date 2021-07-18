using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Point : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public GameObject ES;
    private EnemySpawn enemySpawn;
    private int Score;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawn = ES.GetComponent<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        Score = GameManager.Instance.EnemyKilled;
        textMesh.SetText(Score.ToString());
    }
}
