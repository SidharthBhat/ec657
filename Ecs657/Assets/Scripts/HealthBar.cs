using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    // Start is called before the first frame update

    //increase max hp and set HP to that amount
    public void setMaxHealth(float maxHealth)
	{
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
	}

    public void setHealth(int health)
	{
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
