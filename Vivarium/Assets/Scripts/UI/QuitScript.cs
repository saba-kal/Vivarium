using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Closes the application when the Quit button is clicked.
/// </summary>
public class QuitScript : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
   
}
