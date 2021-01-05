using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TileGridController : MonoBehaviour
{
    public delegate void GridCellClick(Tile tile);
    public static event GridCellClick OnGridCellClick;

    public static TileGridController Instance { get; private set; }

    //Public properties.
    public int GridWidth;
    public int GridHeight;
    public float GridCellSize;
    public string GridData;
    public Vector3 GridOrigin;
    public GameObject PrimaryHighlightPrefab;
    public GameObject SecondaryHighlightPrefab;
    public GameObject TertiaryHighlightPrefab;

    //Private properties.
    private Grid<Tile> _grid;
    private Vector3? _mousePosition;
    private Tile _mouseHoverTile;
    private GameObject _mouseHoverHighlightObject;
    private GridHighlightRank? _mouseTrackedHighlight;
    private Dictionary<GridHighlightRank, List<GameObject>> _gridHighlightObjects = new Dictionary<GridHighlightRank, List<GameObject>>();
    private List<CharacterController> _allCharacters = new List<CharacterController>();

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        _mouseHoverHighlightObject = Instantiate(PrimaryHighlightPrefab, transform);
        _mouseHoverHighlightObject.SetActive(false);
        StoreCharacterControllers(Constants.ENEMY_CHAR_TAG);
        StoreCharacterControllers(Constants.PLAYER_CHAR_TAG);
    }

    private void Update()
    {
        GetMousePosition();
        ShowMouseHover();
        HandleMouseClick();
    }

    public void Initialize(Grid<Tile> grid = null)
    {
        if (grid == null && string.IsNullOrEmpty(GridData))
        {
            return;
        }

        if (grid == null)
        {
            _grid = GridSerializer.Deserialize(GridData);
            if (_grid == null)
            {
                Debug.LogError("There was an error deserializing the grid data.");
            }
        }
        else
        {
            _grid = grid;
            GridData = GridSerializer.Serialize(_grid);
        }

        GridWidth = _grid.GetGrid().GetLength(0);
        GridHeight = _grid.GetGrid().GetLength(1);
        GridCellSize = _grid.GetCellSize();
        GridOrigin = _grid.GetOrigin();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ClearGridData()
    {
        GridData = null;
        _grid = null;
    }

    public bool GridIsGenerated()
    {
        return GridData != null && _grid != null;
    }

    private void StoreCharacterControllers(string tag)
    {
        var characterObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var characterObject in characterObjects)
        {
            var characterController = characterObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                _allCharacters.Add(characterController);
            }
        }
    }

    private void GetMousePosition()
    {
        //Create ray from mouse position on the camera.
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Cast the ray to see if it hit anything.
        if (Physics.Raycast(ray, out var hit))
        {
            _mousePosition = hit.point;
        }
        else
        {
            _mousePosition = null;
        }
    }

    private void ShowMouseHover()
    {
        if (_mousePosition == null)
        {
            _mouseHoverHighlightObject.SetActive(false);
            _mouseHoverTile = null;
            return;
        }

        var gridCellPosition = _grid.ConvertToGridCellPosition(_mousePosition.Value);
        gridCellPosition.y += 0.01f; //Slightly raise the position to avoid clipping with ground.

        _mouseHoverTile = _grid.GetValue(_mousePosition.Value);

        _mouseHoverHighlightObject.SetActive(true);
        _mouseHoverHighlightObject.transform.position = gridCellPosition;
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current == null)
            {
                Debug.LogError("This scene is missing an event system. Please create an event system game object.");
                return;
            }
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            SoundManager.GetInstance()?.Play(Constants.GRID_CELL_CLICK_SOUND);
            OnGridCellClick?.Invoke(_mouseHoverTile);
        }
    }

    public Grid<Tile> GetGrid()
    {
        return _grid;
    }

    public void RemoveHighlights(GridHighlightRank highlightRank)
    {
        if (_gridHighlightObjects.TryGetValue(highlightRank, out var highlightObjects))
        {
            foreach (var highlightObject in highlightObjects)
            {
                Destroy(highlightObject);
            }
            _gridHighlightObjects[highlightRank] = new List<GameObject>();
        }
        if (_mouseTrackedHighlight == highlightRank)
        {
            _mouseTrackedHighlight = null;
        }
    }

    public Dictionary<(int, int), Tile> HighlightRadius(int x, int y, float minRadius, float maxRadius, GridHighlightRank highlightRank)
    {
        return HighlightRadius(x, y, minRadius, maxRadius, highlightRank, new Dictionary<(int, int), Tile>(), new Dictionary<(int, int), Tile>(), x, y);
    }

    private Dictionary<(int, int), Tile> HighlightRadius(
        int x,
        int y,
        float minRadius,
        float maxRadius,
        GridHighlightRank highlightRank,
        Dictionary<(int, int), Tile> existingHighlightedTiles,
        Dictionary<(int, int), Tile> visitedTiles,
        int initialX,
        int initialY)
    {
        if (visitedTiles.ContainsKey((x, y)))
        {
            return existingHighlightedTiles;
        }

        var tile = _grid.GetValue(x, y);
        if (tile == null)
        {
            return existingHighlightedTiles;
        }

        var sqrDistance = Vector2.SqrMagnitude(new Vector2(initialX - x, initialY - y));
        if (sqrDistance > maxRadius * maxRadius)
        {
            return existingHighlightedTiles;
        }

        if (sqrDistance >= minRadius * minRadius)
        {
            existingHighlightedTiles.Add((x, y), tile);
            CreateHighlightObject(x, y, highlightRank);
        }
        visitedTiles.Add((x, y), tile);

        HighlightRadius(x, y + 1, minRadius, maxRadius, highlightRank, existingHighlightedTiles, visitedTiles, initialX, initialY); //Up
        HighlightRadius(x, y - 1, minRadius, maxRadius, highlightRank, existingHighlightedTiles, visitedTiles, initialX, initialY); //Down
        HighlightRadius(x + 1, y, minRadius, maxRadius, highlightRank, existingHighlightedTiles, visitedTiles, initialX, initialY); //Left
        HighlightRadius(x - 1, y, minRadius, maxRadius, highlightRank, existingHighlightedTiles, visitedTiles, initialX, initialY); //Right

        return existingHighlightedTiles;
    }

    public Dictionary<(int, int), Tile> GetTilesInRadius(int x, int y, float minRadius, float maxRadius)
    {
        return GetTilesInRadius(x, y, minRadius, maxRadius, new Dictionary<(int, int), Tile>(), new Dictionary<(int, int), Tile>(), x, y);
    }

    private Dictionary<(int, int), Tile> GetTilesInRadius(
        int x,
        int y,
        float minRadius,
        float maxRadius,
        Dictionary<(int, int), Tile> tilesInRadius,
        Dictionary<(int, int), Tile> visitedTiles,
        int initialX,
        int initialY)
    {
        if (visitedTiles.ContainsKey((x, y)))
        {
            return tilesInRadius;
        }

        var tile = _grid.GetValue(x, y);
        if (tile == null)
        {
            return tilesInRadius;
        }

        var sqrDistance = Vector2.SqrMagnitude(new Vector2(initialX - x, initialY - y));
        if (sqrDistance > maxRadius * maxRadius)
        {
            return tilesInRadius;
        }

        if (sqrDistance >= minRadius * minRadius)
        {
            tilesInRadius.Add((x, y), tile);
        }
        visitedTiles.Add((x, y), tile);

        GetTilesInRadius(x, y + 1, minRadius, maxRadius, tilesInRadius, visitedTiles, initialX, initialY); //Up
        GetTilesInRadius(x, y - 1, minRadius, maxRadius, tilesInRadius, visitedTiles, initialX, initialY); //Down
        GetTilesInRadius(x + 1, y, minRadius, maxRadius, tilesInRadius, visitedTiles, initialX, initialY); //Left
        GetTilesInRadius(x - 1, y, minRadius, maxRadius, tilesInRadius, visitedTiles, initialX, initialY); //Right

        return tilesInRadius;
    }

    public void HighlightTiles(Dictionary<(int, int), Tile> tiles, GridHighlightRank highlightRank)
    {
        foreach (var tileKeyValue in tiles)
        {
            CreateHighlightObject(tileKeyValue.Value.GridX, tileKeyValue.Value.GridY, highlightRank);
        }
    }

    private void CreateHighlightObject(int x, int y, GridHighlightRank highlightRank)
    {
        var tileHighlight = Instantiate(GetHighlightPrefab(highlightRank), transform);
        tileHighlight.SetActive(true);
        var targetPosition = _grid.GetWorldPositionCentered(x, y);
        targetPosition.y += 0.01f;
        tileHighlight.transform.position = targetPosition;
        if (_gridHighlightObjects.ContainsKey(highlightRank))
        {
            _gridHighlightObjects[highlightRank].Add(tileHighlight);
        }
        else
        {
            _gridHighlightObjects.Add(highlightRank, new List<GameObject> { tileHighlight });
        }
    }

    private GameObject GetHighlightPrefab(GridHighlightRank highlightRank)
    {
        switch (highlightRank)
        {
            case GridHighlightRank.Primary:
                return PrimaryHighlightPrefab;
            case GridHighlightRank.Secondary:
                return SecondaryHighlightPrefab;
            case GridHighlightRank.Tertiary:
                return TertiaryHighlightPrefab;
            default:
                return PrimaryHighlightPrefab;
        }
    }

    public Tile GetClosestTile(
        Vector3 fromPosition,
        Vector3 toPosition,
        float range,
        out float resultDistance)
    {
        var gridTileInLine = _grid.GetLine(fromPosition, toPosition);
        Tile closestTile = null;
        resultDistance = float.MaxValue;

        foreach (var tile in gridTileInLine)
        {
            var tileWorldPos = _grid.GetWorldPositionCentered(tile.GridX, tile.GridY);
            var distance = Vector3.Distance(tileWorldPos, fromPosition);
            if (distance < range && !TileContainsCharacter(tile))
            {
                closestTile = tile;
                resultDistance = distance;
            }
        }

        return closestTile;
    }

    public bool TileContainsCharacter(Tile tile)
    {
        foreach (var character in _allCharacters)
        {
            _grid.GetGridCoordinates(character.transform.position, out var x, out var y);
            if (tile.GridX == x && tile.GridY == y)
            {
                return true;
            }
        }

        return false;
    }

    public Tile GetMouseHoverTile()
    {
        return _mouseHoverTile;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        var testGrid = new Grid<Tile>(GridWidth, GridHeight, GridCellSize, GridOrigin, (int x, int y, Grid<Tile> grid) => new Tile(x, y, grid));
        testGrid.PreviewGrid();
    }
}

public enum GridHighlightRank
{
    Primary = 1,
    Secondary = 2,
    Tertiary = 3,
}
