using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraController : MonoBehaviour
{
    void OnEnable()
    {
        PlayerController.OnCharacterSelect += OnCharacterSelect;
    }

    void OnDisable()
    {
        PlayerController.OnCharacterSelect -= OnCharacterSelect;
    }

    private void OnCharacterSelect(CharacterController selectedCharacter)
    {
        transform.position = selectedCharacter.transform.position;
    }
}
