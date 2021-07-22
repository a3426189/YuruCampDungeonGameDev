using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image Fill;
    // Start is called before the first frame update

    private float TempCurrentHealth;
    private void Start()
    {
        //testing 
        SetMaxHealth(100);
        SetCurrentHealth(100);
        TempCurrentHealth = slider.value;
    }
    private void Update()
    {
        if(slider.value != TempCurrentHealth)
        {
            TempCurrentHealth = slider.value;
            SetCurrentHealth(TempCurrentHealth);
        }
    }
    public void SetCurrentHealth(float health)
    {
        slider.value = health;
        float MaxHealth = slider.maxValue;
        Fill.color = gradient.Evaluate(slider.normalizedValue);//set to 0~1
    }
    public void SetMaxHealth(float MaxHealth)
    {
        slider.maxValue = MaxHealth;
        Fill.color = gradient.Evaluate(1f);
    }
}
