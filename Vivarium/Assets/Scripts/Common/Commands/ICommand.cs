using UnityEngine;
using UnityEditor;
using System.Collections;

public interface ICommand
{
    IEnumerator Execute();
}