using UnityEngine;
using System.Collections;

/// <summary>
/// Common Utility Functions
/// </summary>
public static class Utils
{
    /// <summary>
    /// Finds all child object with a specified tag
    /// </summary>
    /// <param name="parent">The parent game object</param>
    /// <param name="tag">The specified tag to search for</param>
    /// <returns></returns>
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

    /// <summary>
    /// Sees if the animator has a parameter
    /// </summary>
    /// <param name="paramName">The parameter name to check for</param>
    /// <param name="animator">The animator to search in</param>
    /// <returns></returns>
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
