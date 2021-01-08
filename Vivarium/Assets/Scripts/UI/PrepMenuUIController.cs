using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.UI;

public class PrepMenuUIController : MonoBehaviour
{
    public delegate void BackClick();
    public static event BackClick OnBackClick;

    public Button BackButton;
    public Button InventoryButton;
    public Dictionary<string, Sprite> CharacterPortraits = new Dictionary<string, Sprite>();

    private void Start()
    {
        BackButton.onClick.AddListener(() => OnBackClick?.Invoke());
        GetCharacterPortraits();
    }

    public void GetCharacterPortraits()
    {
        for (int i = 0; i < TurnSystemManager.Instance.PlayerController.PlayerCharacters.Count; i++)
        {
            CharacterPortraits.Add(TurnSystemManager.Instance.PlayerController.PlayerCharacters[i].Character.Id, TurnSystemManager.Instance.PlayerController.PlayerCharacters[i].Character.Portrait);
        }
    }
}