using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float _health;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
