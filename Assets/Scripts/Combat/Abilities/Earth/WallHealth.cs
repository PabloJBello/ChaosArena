using UnityEngine;

/* Makes walls destructible. */
public class WallHealth : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;

    public void Initialize(float health)
    {
        maxHealth = health;
        currentHealth = health;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            DestroyWall();
        }
    }

    private void DestroyWall()
    {
        Debug.Log($"{gameObject.name} destroyed!");
        Destroy(gameObject);
    }
}