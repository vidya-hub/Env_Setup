using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void setMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void setCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }

}
