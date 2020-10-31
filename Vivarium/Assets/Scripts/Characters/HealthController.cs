using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public HealthBar HealthBar;
    public HealthBar ShieldBar;

    private float _currentHealth;
    private float _shieldHealth;

    public void SetHealthStats(float currentHealth, float maxHealth, float shieldHealth, float maxShield)
    {
        _currentHealth = currentHealth;
        _shieldHealth = shieldHealth;

        HealthBar.SetMaxHealth(maxHealth);
        HealthBar.SetHealth(currentHealth);
        ShieldBar?.SetMaxHealth(maxShield);
        ShieldBar?.SetHealth(shieldHealth);
    }

    public bool TakeDamage(float damage)
    {
        if (_shieldHealth > 0)
        {
            if (_shieldHealth >= damage)
            {
                _shieldHealth -= damage;
                damage = 0;
            }
            else
            {
                damage -= _shieldHealth;
                _shieldHealth = 0;
            }
         
        }
        _currentHealth -= damage;
        HealthBar.SetHealth(_currentHealth);
        ShieldBar.SetHealth(_shieldHealth);
        return _currentHealth <= 0;
    }
}
