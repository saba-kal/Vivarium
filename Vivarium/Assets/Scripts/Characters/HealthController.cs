using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public HealthBar HealthBar;

    private float _currentHealth;

    public void SetHealthStats(float currentHealth, float maxHealth)
    {
        _currentHealth = currentHealth;
        HealthBar.SetMaxHealth(maxHealth);
        HealthBar.SetHealth(currentHealth);
    }

    public bool TakeDamage(float damage)
    {
        _currentHealth -= damage;
        HealthBar.SetHealth(_currentHealth);
        return _currentHealth <= 0;
    }
}
