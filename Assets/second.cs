using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class second : MonoBehaviour
{
    float time = 0.0f;
    public float timemaxToNextDiff = 10.0f;
    
    public TextMeshProUGUI SecondText;
    // Update is called once per frame
    void Update()
    {
        SecondText.text = time.ToString("F2");
        time += Time.deltaTime;
        if (time >= timemaxToNextDiff)
        {
            time -= timemaxToNextDiff;
        }
    }
}
