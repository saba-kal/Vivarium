using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public HealthBar HealthBar;
    public HealthBar ShieldBar;

    private float _currentHealth;
<<<<<<< Updated upstream
    private float _shieldHealth;
=======
    private float _maxHealth;
>>>>>>> Stashed changes

    public void SetHealthStats(float currentHealth, float maxHealth, float shieldHealth, float maxShield)
    {
        _currentHealth = currentHealth;
<<<<<<< Updated upstream
        _shieldHealth = shieldHealth;

=======
        _maxHealth = maxHealth;
>>>>>>> Stashed changes
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

    public void Healing(float heal)
    {
        if (_currentHealth + heal <= _maxHealth)
        {
            _currentHealth += heal;
            HealthBar.SetHealth(_currentHealth);
        }
        else
        {
            _currentHealth = _maxHealth;
            HealthBar.SetHealth(_currentHealth);
        }
    }
}
