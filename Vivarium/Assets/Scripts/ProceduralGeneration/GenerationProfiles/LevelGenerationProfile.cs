using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Level Profile", menuName = "Level Profile", order = 1)]
public class LevelGenerationProfile : ScriptableObject
{
    public List<TileDisplayInfo> TileInfos;

    public int MinGridWidth;
    public int MaxGridWidth;

    public int MinGridHeight;
    public int MaxGridHeight;

    public float GridCellSize;
    public Vector3 GridOrigin;

    public GameObject PrimaryHighlightPrefab;
    public GameObject SecondaryHighlightPrefab;
    public GameObject TertiaryHighlightPrefab;

    public int MinEnemyCharacters;
    public int MaxEnemyCharacters;
    public List<CharacterGenerationProfile> PossibleEnemyCharacters;

    public int MinPlayerCharacters;
    public int MaxPlayerCharacters;
    public List<CharacterGenerationProfile> PossiblePlayerCharacters;
}
