using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_animation_trigger : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Reset the "Crouch" trigger
            myAnimator.ResetTrigger("Swerve");

            //Send the message to the Animator to activate the trigger parameter named "Jump"
            myAnimator.SetTrigger("Jump");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Reset the "Crouch" trigger
            myAnimator.ResetTrigger("Jump");

            //Send the message to the Animator to activate the trigger parameter named "Jump"
            myAnimator.SetTrigger("Swerve");
        }
    }
}
