using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public HealthBar HealthBar;
    public HealthBar ShieldBar;

    private float _currentHealth;
    private float _shieldHealth;
    private float _maxHealth;
    private float _maxShield;

    public void SetHealthStats(float currentHealth, float maxHealth, float shieldHealth, float maxShield)
    {
        _currentHealth = currentHealth;
        _shieldHealth = shieldHealth;
        _maxHealth = maxHealth;
        _maxShield = maxShield;

        HealthBar.SetMaxHealth(maxHealth);
        HealthBar.SetHealth(currentHealth);
        ShieldBar?.SetMaxHealth(maxShield);
        ShieldBar?.SetHealth(shieldHealth);
        UpdateShieldDisplay();
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
        ShieldBar?.SetHealth(_shieldHealth);
        UpdateShieldDisplay();
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

    public void RegenerateShield(float shieldAmount)
    {
        if (_shieldHealth + shieldAmount <= _maxShield)
        {
            _shieldHealth += shieldAmount;
            ShieldBar?.SetHealth(_shieldHealth);
        }
        else
        {
            _shieldHealth = _maxShield;
            ShieldBar?.SetHealth(_shieldHealth);
        }
        UpdateShieldDisplay();
    }

    public void UpdateShieldDisplay()
    {
        if (_shieldHealth <= 0)
        {
            ShieldBar?.gameObject.SetActive(false);
        }
        else
        {
            ShieldBar?.gameObject.SetActive(true);
        }
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetCurrentShield()
    {
        return _shieldHealth;
    }
}
