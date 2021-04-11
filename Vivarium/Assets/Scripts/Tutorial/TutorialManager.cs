using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }
    public Queue<string> tutorialPrompts;
    public GameObject screen;
    public Button nextButton;
    private static bool IsTutorial = false;
    private int index;

    private bool wPress = false;
    private bool aPress = false;
    private bool sPress = false;
    private bool dPress = false;
    private bool qPress = false;
    private bool ePress = false;
    private bool scrollIn = false;
    private bool scrollOut = false;

    private CharacterController enemy;
    private CharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        tutorialPrompts = new Queue<string>();
        TutorialManager.UpdateScreen();
        nextButton.interactable = false;
        enemy = LevelManager.Instance.LevelGenerator.GetEnemyAIManager().AICharacters[0];
        player = LevelManager.Instance.LevelGenerator.PlayerCharacters[0];
    }

    private void Update()
    {
        if(IsTutorial)
        {
            switch (index)
            {
                case 0:
                    Index0();
                    break;
                case 1:
                    Index1();
                    break;
                case 2:
                    Index2();
                    break;
                case 3:
                    Index3();
                    break;
                case 5:
                    Index5();
                    break;
                case 6:
                    Index6();
                    break;
            }
        }
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

    private void Index0()
    {
        if (Input.GetKeyDown("w"))
        {
            wPress = true;
        }
        else if (Input.GetKeyDown("a"))
        {
            aPress = true;
        }
        else if (Input.GetKeyDown("s"))
        {
            sPress = true;
        }
        else if (Input.GetKeyDown("d"))
        {
            dPress = true;
        }
        if (wPress && aPress && sPress && dPress && !nextButton.interactable)
        {
            nextButton.interactable = true;
        }
    }

    private void Index1()
    {
        if (Input.GetKeyDown("q"))
        {
            qPress = true;
        }
        else if (Input.GetKeyDown("e"))
        {
            ePress = true;
        }
        if (qPress && ePress && !nextButton.interactable)
        {
            nextButton.interactable = true;
        }
    }

    private void Index2()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            scrollIn = true;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            scrollOut = true;
        }
        if (scrollIn && scrollOut && !nextButton.interactable)
        {
            nextButton.interactable = true;
        }
    }

    private void Index3()
    {
        if(enemy.getIsSelected())
        {
            nextButton.interactable = true;
        }
    }

    public void Index4()
    {
        if(index == 4)
        {
            nextButton.interactable = true;
        }
    }

    private void Index5()
    {
        if(player.getIsSelected())
        {
            nextButton.interactable = true;
        }
    }

    private void Index6()
    {
        if (!player.IsAbleToMove())
        {
            nextButton.interactable = true;
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

    public void UpdateIndex(int num)
    {
        index += num;
    }

    public int GetCurrentIndex()
    {
        return index;
    }
}
