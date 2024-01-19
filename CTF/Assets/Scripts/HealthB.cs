using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthB : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text healthText;
    private String[] tmp;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        tmp = healthText.text.Split(':');
        healthText.text = tmp[0] + ":" + health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        healthText.text = tmp[0] + ":" + health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
