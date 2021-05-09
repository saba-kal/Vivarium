using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Handles the health bar UI of characters. Used for both health and shields.
/// </summary>
[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    public Slider HealthBarSlider;
    public TextMeshProUGUI HealthBarText;
    public bool isShieldBar;
    public GameObject HealthChangeEffectPrefab;
    public float HealthChangeEffectLifeTime = 1f;
    public float HealthChangeEffectSpeed = 1f;
    public Color PositiveHealthChangeColor = Color.green;
    public Color NegativeHealthChangeColor = Color.red;

    private float _maxHealth = 0f;
    private float _currentHealth = 0f;
    private List<GameObject> _healthChangeEffects = new List<GameObject>();

    private void Update()
    {
        for (int i = 0; i < _healthChangeEffects.Count; i++)
        {
            if (_healthChangeEffects[i] == null)
            {
                _healthChangeEffects.RemoveAt(i);
            }
            else
            {
                var healthChangeEffect = _healthChangeEffects[i];
                healthChangeEffect.transform.Translate(new Vector3(0, HealthChangeEffectSpeed * Time.deltaTime, 0), Space.Self);

                var textObject = healthChangeEffect.GetComponentInChildren<TextMeshProUGUI>();
                if (textObject != null)
                {
                    textObject.color = new Color(
                        textObject.color.r,
                        textObject.color.g,
                        textObject.color.b,
                        textObject.color.a - HealthChangeEffectSpeed * Time.deltaTime);
                }
            }
        }
    }

    /// <summary>
    /// Changes the max health a health bar can have. 
    /// </summary>
    /// <param name="maxHealth">The max health to set to.</param>
    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        HealthBarSlider.maxValue = maxHealth;
        UpdateHealthBarText();
    }

    /// <summary>
    /// Changes the current health a health bar has.
    /// </summary>
    /// <param name="health">The health value to set current health to.</param>
    public void SetHealth(float health)
    {
        _currentHealth = health;
        HealthBarSlider.value = health;
        UpdateHealthBarText();
    }

    private void UpdateHealthBarText()
    {
        if (HealthBarText != null)
        {
            HealthBarText.text = $"{_currentHealth:n0}";
        }
    }
    /// <summary>
    /// Displays a visual effect on the health bar when the current health value changes.
    /// </summary>
    /// <param name="healthChangeAmount">The number amount of health that is changed from the current health.</param>
    public void ShowChangeHealthEffect(float healthChangeAmount)
    {
        var healthChangeEffect = Instantiate(HealthChangeEffectPrefab, transform);
        Destroy(healthChangeEffect, HealthChangeEffectLifeTime);

        var textObject = healthChangeEffect.GetComponentInChildren<TextMeshProUGUI>();
        if (textObject != null)
        {
            var sign = healthChangeAmount > 0 ? "+" : "-";
            var color = healthChangeAmount > 0 ? PositiveHealthChangeColor : NegativeHealthChangeColor;

            textObject.text = $"{sign}{Math.Abs(healthChangeAmount):n0}";
            textObject.color = color;

            _healthChangeEffects.Add(healthChangeEffect);
        }
        else
        {
            Debug.LogError("The health change effect prefab does not have a Text Mesh Pro component to display text");
        }
    }
}
