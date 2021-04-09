using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }
    public Queue<string> tutorialPrompts;
    public GameObject screen;
    private static bool IsTutorial = false;
    // Start is called before the first frame update
    void Start()
    {
        tutorialPrompts = new Queue<string>();
        TutorialManager.UpdateScreen();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Skip()
    {
        var levelManager = LevelManager.Instance;
        if (levelManager != null)
        {
            levelManager.CompleteLevel();
        }
    }

    public static bool GetIsTutorial()
    {
        return IsTutorial;
    }

    public static void SetIsTutorial(bool isTutorial)
    {
        IsTutorial = isTutorial;
    }

    public static void UpdateScreen()
    {
        var tutorialManager = TutorialManager.Instance;
        var screen = tutorialManager.screen;
        if (screen != null)
        {
            if (IsTutorial)
            {
                screen.SetActive(true);
            }
            else
            {
                screen.SetActive(false);
            }
        }
    }
}
