using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public HealthBar HealthBar;
    public HealthBar ShieldBar;

    private float _currentHealth;
    private float _currentShield;
    private float _maxHealth;
    private float _maxShield;
    private bool _hasTakenDamage = false;

    public void SetHealthStats(float currentHealth, float maxHealth, float shieldHealth, float maxShield)
    {
        _currentHealth = currentHealth;
        _currentShield = shieldHealth;
        _maxHealth = maxHealth;
        _maxShield = maxShield;

        HealthBar.SetMaxHealth(maxHealth);
        HealthBar.SetHealth(currentHealth);
        ShieldBar?.SetMaxHealth(maxShield);
        ShieldBar?.SetHealth(shieldHealth);
        UpdateShieldDisplay();
    }

    /// <summary>
    /// Removes points from health and shield bar based on damage taken.
    /// </summary>
    /// <param name="damage">The amount of damage that was dealt to this character.</param>
    /// <returns>Whether or not the character lost all health.</returns>
    public bool TakeDamage(float damage)
    {
        if (_currentShield > 0)
        {
            if (_currentShield >= damage)
            {
                _currentShield -= damage;
                damage = 0;
            }
            else
            {
                damage -= _currentShield;
                _currentShield = 0;
            }

        }
        _currentHealth -= damage;
        HealthBar.SetHealth(_currentHealth);
        HealthBar.ShowChangeHealthEffect(-damage);
        ShieldBar?.SetHealth(_currentShield);
        UpdateShieldDisplay();
        PerformFlinchAnimation();
        _hasTakenDamage = true;

        return _currentHealth < 1;
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
        HealthBar.ShowChangeHealthEffect(heal);
    }

    public void RegenerateShield(float shieldAmount)
    {
        if (_currentShield + shieldAmount <= _maxShield)
        {
            _currentShield += shieldAmount;
            ShieldBar?.SetHealth(_currentShield);
        }
        else
        {
            _currentShield = _maxShield;
            ShieldBar?.SetHealth(_currentShield);
        }
        UpdateShieldDisplay();
    }

    public void UpgradMaxShield(float newMaxShield)
    {
        if (_currentShield == _maxShield || _currentShield > newMaxShield)
        {
            _currentShield = newMaxShield;
        }
        _maxShield = newMaxShield;
        ShieldBar?.SetMaxHealth(_maxShield);
        ShieldBar?.SetHealth(_currentShield);

        UpdateShieldDisplay();
    }

    private void UpdateShieldDisplay()
    {
        if (_currentShield <= 0 || _maxShield <= 0)
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
        return _currentShield;
    }

    public void RemoveShield()
    {
        _maxShield = 0;
        _currentShield = 0;
        UpdateShieldDisplay();
    }

    private void PerformFlinchAnimation()
    {
        var animationTypeName = System.Enum.GetName(typeof(AnimationType), AnimationType.flinch);
        Animator myAnimator = gameObject.GetComponentInChildren<Animator>();
        myAnimator.SetTrigger(animationTypeName);
    }

    /// <summary>
    /// Determines whether or not the character with this health controller has taken damage.
    /// </summary>
    /// <returns>Boolean indicating whether or not the character has taken damage.</returns>
    public bool HasTakenDamage()
    {
        return _hasTakenDamage;
    }
}
