using UnityEngine;
using System.Collections;
using TMPro;

public class TextLabel : MonoBehaviour
{
    public TextMeshProUGUI TextObject;

    public void SetText(string text)
    {
        TextObject.text = text;
    }
}
