using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    public Slider HealthBarSlider;
    public TextMeshProUGUI HealthBarText;

    private float _maxHealth = 0f;
    private float _currentHealth = 0f;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
    }

    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        HealthBarSlider.maxValue = maxHealth;
        UpdateHealthBarText();
    }

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
            HealthBarText.text = $"{_currentHealth:n0}/{_maxHealth:n0}";
        }
    }
}
