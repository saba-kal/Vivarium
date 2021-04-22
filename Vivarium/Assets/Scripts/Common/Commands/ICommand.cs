using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// Defines a command that will execute in the command queue
/// </summary>
public interface ICommand
{
    IEnumerator Execute();
}