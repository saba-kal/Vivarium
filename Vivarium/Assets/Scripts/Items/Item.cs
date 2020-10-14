using UnityEngine;
using System.Collections;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.UI;
using System;

[Serializable]
public class Item : ScriptableObject
{
    public string Id;
    public string Name;
    public string Description;
    public ItemType Type;
    public Image Icon;
    public GameObject Model;
}
