using UnityEngine;

public class SpaceJumper : MonoBehaviour{

    public int maxHealth = 100;
    public float currentHealth;
    public HealthB healthB;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthB.SetMaxHealth(maxHealth);
    }
    
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthB.SetHealth((int)currentHealth);
    }
    
    public int getCurrentHealth()
    {
        return ((int)currentHealth);
    }
}
