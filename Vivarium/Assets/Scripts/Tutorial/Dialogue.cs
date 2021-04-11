using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI buttonText;
    public GameObject skipButton;
    public GameObject dialogBox;
    public Button nextButton;
    private List<string> prompts;
    private int index;
    public float BaseHeight = 100f;

    private void Start()
    {
        CreatePrompts();
        textDisplay.text = prompts[0];
    }

    public void CreatePrompts()
    {
        prompts = new List<string>();
        prompts.Add("Use WASD to pan the camera.");
        prompts.Add("Use Q and E to rotate the camera.");
        prompts.Add("Scroll forward or backward on your mousewheel to zoom the camera in and out.");
        prompts.Add("Left clicking an enemy (outlined in purple) shows their movement range and inventory");
        prompts.Add("Checking the \"Enemy Threat Range\" box toggles on or off tile highlights that show where an enemy could attack after moving");
        prompts.Add("Left clicking your character (outlined in white) selects it");
        prompts.Add("The highlighted tiles are all the places your character can move. Left click a tile next to the enemy while your character is selected to move there.");
        prompts.Add("Select an action from the action menu to perform that action. Click \"Slash\" on the right");
        prompts.Add("With your attack selected, select an enemy in range to attack them. Tiles within your range are highlighted in red");
        prompts.Add("Each character can only use one action and one movement per turn.  Clicking \"End Turn\" allows enemies to take their next turn");
        prompts.Add("Your goal is to get one character to the exit. The exit is located in the far right corner of the map");
        prompts.Add("Remember, if a character dies before you make it to the exit, it doesn't come back and you've lost that character forever. Good luck and have fun!");
    }

    public void NextPrompt()
    {
        if (index < prompts.Count - 1)
        {
            index++;
            TutorialManager.Instance.UpdateIndex(1);
            textDisplay.text = prompts[index];
            if (index == prompts.Count - 1)
            {
                UpdateButtons();
            }
            nextButton.interactable = false;
        }
        else if (index == prompts.Count - 1)
        {
            EndLevel();
        }

    }

    private void UpdateButtons()
    {
        buttonText.text = "End Tutorial";
        skipButton.SetActive(false);
        //Update to remove skip tutorial button
    }

    public void EndLevel()
    {
        dialogBox.SetActive(false);
        var levelManager = LevelManager.Instance;
        if (levelManager != null)
        {
            levelManager.CompleteLevel();
        }
    }
}