using UnityEngine;
using System.Collections;
using System.Linq;

public class RewardsChest : MonoBehaviour
{

    public LootTable Loot;

    public Item Open()
    {
        return Loot.Pick(1).FirstOrDefault();
    }
}
