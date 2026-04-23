using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public int moneyValue = 10; 

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}