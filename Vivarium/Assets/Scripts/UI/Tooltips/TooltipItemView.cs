using UnityEngine;
using System.Collections;
using TMPro;

public class TooltipItemView : MonoBehaviour
{
    public TextMeshProUGUI TooltipTitle;
    public TextMeshProUGUI TooltipDescription;

    public void DisplayItem(Item item)
    {
        TooltipTitle.text = item.Name;
        TooltipDescription.text = item.Description;
    }
}
