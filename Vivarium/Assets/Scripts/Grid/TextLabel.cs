using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// Shows a text label on a grid cell.
/// </summary>
public class TextLabel : MonoBehaviour
{
    public TextMeshProUGUI TextObject;

    /// <summary>
    /// Sets the text to show.
    /// </summary>
    /// <param name="text">The text to show.</param>
    public void SetText(string text)
    {
        TextObject.text = text;
        if (int.TryParse(text, out var integer))
        {
            var lerpAmount = integer / 400f;
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
