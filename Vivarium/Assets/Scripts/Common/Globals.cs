using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour
{
    public static Globals Instance { get; private set; }

    public float CharacterMoveSpeed = 10f;
    public string EnemyTag = "enemy";
    public string PlayerCharacterTag = "playerCharacter";

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
}
