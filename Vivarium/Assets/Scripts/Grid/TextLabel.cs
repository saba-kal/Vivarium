using UnityEngine;
using System.Collections;
using TMPro;

public class TextLabel : MonoBehaviour
{
    public TextMeshProUGUI TextObject;

    public void SetText(string text)
    {
        TextObject.text = text;
        if (int.TryParse(text, out var integer))
        {
            var lerpAmount = integer / 200f;
            if (integer < 0)
            {
                TextObject.color = Color.Lerp(Color.white, Color.red, -lerpAmount);
            }
            else
            {
                TextObject.color = Color.Lerp(Color.white, Color.green, lerpAmount);
            }
        }
    }
}
