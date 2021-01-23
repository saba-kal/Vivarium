using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompleteLevelOnButtonClick : MonoBehaviour
{
    public Button ButtonReference;

    // Use this for initialization
    void Start()
    {
        ButtonReference.gameObject.SetActive(false);
        ButtonReference.onClick.AddListener(() =>
        {
            var levelManager = LevelManager.Instance;
            if (levelManager != null)
            {
                levelManager.CompleteLevel();
                ButtonReference.gameObject.SetActive(false);
                UIController.Instance?.HideCharacterInfo();
            }
            else
            {
                Debug.LogError("Level manager instance was null. Unable to complete level");
            }
        });
    }

    void Update()
    {
        var enemyCharacters = TurnSystemManager.Instance?.AIManager?.AICharacters;
        if (enemyCharacters == null || enemyCharacters.Count == 0)
        {
            ButtonReference.gameObject.SetActive(true);
        }
        else
        {
            ButtonReference.gameObject.SetActive(false);
        }
    }
}
