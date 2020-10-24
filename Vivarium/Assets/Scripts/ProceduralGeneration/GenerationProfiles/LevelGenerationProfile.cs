using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Level Profile", menuName = "Level Profile", order = 1)]
public class LevelGenerationProfile : ScriptableObject
{
    public GridGenerationProfile GridProfile;

    public GameObject PrimaryHighlightPrefab;
    public GameObject SecondaryHighlightPrefab;
    public GameObject TertiaryHighlightPrefab;
    public GameObject LevelObjectivePrefab;

    public int MinEnemyCharacters;
    public int MaxEnemyCharacters;
    public List<CharacterGenerationProfile> PossibleEnemyCharacters;

    public int MinPlayerCharacters;
    public int MaxPlayerCharacters;
    public List<CharacterGenerationProfile> PossiblePlayerCharacters;
}
