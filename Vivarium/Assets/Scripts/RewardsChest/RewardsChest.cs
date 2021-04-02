using UnityEngine;
using System.Collections;
using System.Linq;

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

    public void ShowGlow()
    {
        GlowEffect.SetActive(true);
    }

    public void HideGlow()
    {
        GlowEffect.SetActive(false);
    }
}
