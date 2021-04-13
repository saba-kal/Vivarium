using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Command to make a game object perform an animation
/// </summary>
public class PerformAnimationCommand : ICommand
{
    AnimationType _animationType;
    GameObject _gameObject;
    /// <summary>
    /// Command to make a game object perform an animation
    /// </summary>
    /// <param name="gameObject">The game object that performs the animation</param>
    /// <param name="animationType">An Enum that designates which animation to perform</param>
    public PerformAnimationCommand(GameObject gameObject, AnimationType animationType)
    {
        _animationType = animationType;
        _gameObject = gameObject;
    }

    public IEnumerator Execute()
    {
        var animationTypeName = Enum.GetName(typeof(AnimationType), _animationType);

        //var childObject = gameObject.transform.GetChild(0).gameObject;
        Animator myAnimator = _gameObject.GetComponentInChildren<Animator>();
        myAnimator.SetTrigger(animationTypeName);
        yield return null;

    }

}
