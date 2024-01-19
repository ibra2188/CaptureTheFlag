using System;
using UnityEngine;

public class Health : MonoBehaviour{

    public int maxHealth;
    public float currentHealth;
    public HealthB healthB;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthB.SetMaxHealth(maxHealth);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthB.SetHealth((int)currentHealth);
    }

    public void addHealth(float healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthB.SetHealth((int)currentHealth);
    }
    
    
    public int getCurrentHealth()
    {
        return ((int)currentHealth);
    }
}
