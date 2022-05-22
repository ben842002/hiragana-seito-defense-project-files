using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    /// <summary>
    /// Initalizes the maximum amount for the health bar
    /// </summary>
    public void SetMaxHealth(int health)
    {
        // make maxValue equal to parameter int
        slider.maxValue = health;
        slider.value = health;

        // adjust color in editor
        fill.color = gradient.Evaluate(1f);
    }
    /// <summary>
    /// Sets the current health amount for the health bar
    /// </summary>
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
