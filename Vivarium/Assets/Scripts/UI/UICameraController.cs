using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When a character is selected, changes the position of the camera.
/// </summary>
public class UICameraController : MonoBehaviour
{
    private GameObject currentSelectedCharacter;

    private void Start()
    {
        transform.position = Vector3.zero;
    }

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
        if (currentSelectedCharacter != null) {
            changeLayerForParentAndChildren(currentSelectedCharacter, 12);
            currentSelectedCharacter = selectedCharacter.gameObject;
            changeLayerForParentAndChildren(currentSelectedCharacter, 13);
        }
        else
        {
            currentSelectedCharacter = selectedCharacter.gameObject;
            changeLayerForParentAndChildren(currentSelectedCharacter, 13);
        }
        //transform.position = selectedCharacter.transform.position;
        transform.SetParent(selectedCharacter.Model.transform, false);
    }

    private void changeLayerForParentAndChildren(GameObject parentTransform, int layer)
    {
        parentTransform.layer = layer;
        for(var i = 0; i < parentTransform.transform.childCount; i++)
        {
            var child = parentTransform.transform.GetChild(i);
            changeLayerForParentAndChildren(child.gameObject, layer);
        }
    }
}
