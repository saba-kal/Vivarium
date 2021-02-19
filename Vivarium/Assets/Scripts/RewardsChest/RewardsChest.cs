using UnityEngine;
using System.Collections;
using System.Linq;

public class RewardsChest : MonoBehaviour
{
    public GameObject GlowEffect;
    public GameObject SparkleEffect;

    public LootTable Loot;

    private void Start()
    {
        HideGlow();
    }

    public Item Open()
    {
        HideGlow();
        SparkleEffect.SetActive(false);
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
