using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public Queue<string> tutorialPrompts;
    // Start is called before the first frame update
    void Start()
    {
        tutorialPrompts = new Queue<string>();
    }

    public void skip()
    {
        var levelManager = LevelManager.Instance;
        if (levelManager != null)
        {
            levelManager.CompleteLevel();
        }
    }
}
