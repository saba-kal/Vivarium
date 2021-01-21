using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine.SceneManagement;

public class PrepMenuUIController : MonoBehaviour
{
    public delegate void BackClick();
    public static event BackClick OnBackClick;

    public Button BackButton;
    public Button InventoryButton;
    public GameObject PortraitPrefab;
    public GameObject CharacterPortraits;

    private void Start()
    {
        BackButton.onClick.AddListener(() => OnBackClick?.Invoke());
        InsertPortraits();

    }

    public void InsertPortraits()
    {
        int x = -740;
        int y = 250;
        for (int i = 0; i < TurnSystemManager.Instance.PlayerController.PlayerCharacters.Count; i++)
        {
            GameObject Portrait = Instantiate(PortraitPrefab, new Vector3(x, y, 0), Quaternion.identity);
            SceneManager.MoveGameObjectToScene(Portrait, SceneManager.GetSceneByName("InGameUI"));
            Portrait.transform.SetParent(CharacterPortraits.transform, false);
            Portrait.GetComponent<Image>().sprite = TurnSystemManager.Instance.PlayerController.PlayerCharacters[i].Character.Portrait;

            if (i == 3 || i == 7)
            {
                x += 190;
                y = 250;
            }
            else
            {
                y -= 175;
            }
        }
    }
}