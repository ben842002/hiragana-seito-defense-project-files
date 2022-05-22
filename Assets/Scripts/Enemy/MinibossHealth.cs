using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossHealth : MonoBehaviour
{
    public int currentHealth;

    [SerializeField] HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {   

        // find a way to set max health via length of the words the player will have to type
        healthBar.SetMaxHealth(4);
    }

    public void DamageMiniboss(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
