using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopUp : MonoBehaviour
{
    private float disappeartimer;
    private TextMeshPro TextMesh;
    private Color textColor;
    private void Awake()
    {
        TextMesh = transform.GetComponent<TextMeshPro>();
        textColor = TextMesh.color;
        disappeartimer = 1f;
    }
    private void Update()
    {
        float moveYspeed = 2f;
        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;
        disappeartimer -= Time.deltaTime;

        if (disappeartimer < 0)
        {
            float disappearSpeed = 3;
            textColor.a -= disappearSpeed * Time.deltaTime;
            TextMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void setup(float damageAmount)
    {
        TextMesh.SetText(damageAmount.ToString());
    }

}
