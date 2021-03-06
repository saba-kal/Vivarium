using UnityEngine;
using System.Collections;

public static class Utils
{

    public static GameObject FindObjectWithTag(GameObject parent, string tag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.tag == tag)
            {
                return child.gameObject;
            }

            var innerChild = FindObjectWithTag(child.gameObject, tag);
            if (innerChild != null)
            {
                return innerChild;
            }
        }
        return null;
    }

    public static bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
            {
                return true;
            }
        }
        return false;
    }
}
