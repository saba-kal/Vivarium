using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// The chest animations are controlled here along with deciding the item from the chest when it's used
/// </summary>
public class RewardsChest : MonoBehaviour
{
    public GameObject GlowEffect;
    public GameObject SparkleEffect;

    public LootTable Loot;

    private Animator _animator;

    private void Start()
    {
        HideGlow();
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Unable to play chest open animations because the prefab does not have an animator component.");
        }
    }

    /// <summary>
    /// When a chest is opened, the animations for the chest are turned off and an item is picked from the loot table
    /// </summary>
    public Item Open()
    {
        HideGlow();
        SparkleEffect.SetActive(false);
        if (_animator != null)
        {
            _animator.SetTrigger("open");
        }
        return Loot.Pick(1).FirstOrDefault();
    }

    /// <summary>
    /// Turns the glow animation on
    /// </summary>
    public void ShowGlow()
    {
        GlowEffect.SetActive(true);
    }

    /// <summary>
    /// Turns the glow animation off
    /// </summary>
    public void HideGlow()
    {
        GlowEffect.SetActive(false);
    }
}
