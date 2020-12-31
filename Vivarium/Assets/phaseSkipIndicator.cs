using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseSkipIndicator : MonoBehaviour
{
    public GameObject AIManager;

    // Start is called before the first frame update
    void Start()
    {
        var AIManager = GameObject.FindWithTag("AIManager");
        Debug.Log("AIMANAGER: " + AIManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("CURRENT SKIP: " + AIManager.GetComponent<EnemyAIManager>().skipEnemyPhase);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            AIManager.GetComponent<EnemyAIManager>().turnOnSkipEnemyPhase();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            AIManager.GetComponent<EnemyAIManager>().turnOffSkipEnemyPhase();
        }

    }
}
