using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command to pause the queue 
/// </summary>
public class WaitCommand : ICommand
{
    float _waitTime;
    public WaitCommand(float waitTime)
    {
        _waitTime = waitTime;
    }
    /// <summary>
    /// Command to pause the queue for a 0.3 seconds
    /// </summary>
    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(_waitTime);
    }
}
