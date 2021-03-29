using UnityEngine;
using System.Collections;

/// <summary>
/// UI controller that handles trading between two characters.
/// </summary>
public class TradeUIController : MonoBehaviour
{
    public static TradeUIController Instance { get; private set; }

    public GameObject TradeUIGameObject;
    public CharacterDetailsProfile CharacterDetailsPrefab;
    public GameObject CharacterContainer1;
    public GameObject CharacterContainer2;

    private CharacterController _character1;
    private CharacterController _character2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Displays item trade menu between two characters.
    /// </summary>
    /// <param name="character1">The first character controller that's trading.</param>
    /// <param name="character2">The second character controller that's trading.</param>
    public void Display(CharacterController character1, CharacterController character2)
    {
        _character1 = character1;
        _character2 = character2;

        TradeUIGameObject.SetActive(true);

        DisplayCharacterProfile(_character1, CharacterContainer1);
        DisplayCharacterProfile(_character2, CharacterContainer2);
    }

    private void DisplayCharacterProfile(CharacterController character, GameObject container)
    {
        //Clear container.
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }

        var profileObject = Instantiate(CharacterDetailsPrefab, container.transform);
        profileObject.DisplayCharacter(character);
        //profileObject.AddOnDragBeginCallback(OnItemDragStart);
        //profileObject.AddOnDragEndCallback(OnItemDragEnd);
    }
}
