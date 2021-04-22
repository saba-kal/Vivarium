using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command to pause the queue for a number of seconds
/// </summary>
public class WaitCommand : ICommand
{
    /// <summary>
    /// Command to pause the queue for a number of seconds
    /// </summary>
    /// <param name="seconds">the number of seconds to pause the queue</param>
    float _seconds;
    public WaitCommand(float seconds)
    {
        _seconds = seconds;
    }

    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(_seconds);
    }
}
