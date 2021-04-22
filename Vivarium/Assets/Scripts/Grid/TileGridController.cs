using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using System.Linq;

/// <summary>
/// Handles how a <see cref="Grid{T}"/> interacts with the game world.
/// </summary>
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
    public GridVisualSettings VisualSettings;

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
        _mouseHoverHighlightObject = Instantiate(VisualSettings.PrimaryHighlightPrefab, transform);
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

    /// <summary>
    /// Initializes the grid.
    /// </summary>
    /// <param name="grid">The grid object to initialize. If null, one is created.</param>
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

    /// <summary>
    /// Clears all grid save data.
    /// </summary>
    public void ClearGridData()
    {
        GridData = null;
        _grid = null;
    }

    /// <summary>
    /// Gets whether or not the grid has been generated.
    /// </summary>
    /// <returns>Whether or not the grid has been generated.</returns>
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

    /// <summary>
    /// Gets the <see cref="Grid{T}"/> associated with this grid controller.
    /// </summary>
    /// <returns></returns>
    public Grid<Tile> GetGrid()
    {
        return _grid;
    }

    /// <summary>
    /// Removes any highlights that may be on the grid.
    /// </summary>
    /// <param name="highlightRank">The color rank of the highlight.</param>
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

    /// <summary>
    /// Highlights a circle of tiles on the grid.
    /// </summary>
    /// <param name="x">X coordinate of the center of the circle.</param>
    /// <param name="y">Y coordinate of the center of the circle.</param>
    /// <param name="minRadius">Minimum radius of the circle. Tiles below this radius are not highlighted.</param>
    /// <param name="maxRadius">Maximum radius of the circle. Tiles above this radius are not highlighted.</param>
    /// <param name="highlightRank">The color rank of the highlight.</param>
    /// <returns>Position-to-tile dictionary of all the highlighted tiles.</returns>
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

    /// <summary>
    /// Highlights column of tiles.
    /// </summary>
    /// <param name="x">Start x position of the column.</param>
    /// <param name="y">Start x position of the column.</param>
    /// <param name="minColumn">Minimum column height.</param>
    /// <param name="maxColumn">Maximum column height.</param>
    /// <param name="highlightRank">The color rank of the highlight.</param>
    /// <param name="characterPositionX">X position of the character requesting the highlight.</param>
    /// <param name="characterPositionY">Y position of the character requesting the highlight.</param>
    /// <returns>Position-to-tile dictionary of all the highlighted tiles.</returns>
    public Dictionary<(int, int), Tile> HighlightColumn(
        int x,
        int y,
        float minColumn,
        float maxColumn,
        GridHighlightRank highlightRank,
        int characterPositionX,
        int characterPositionY)
    {
        return HighlightColumn(x, y, minColumn, maxColumn, highlightRank, new Dictionary<(int, int), Tile>(), new Dictionary<(int, int), Tile>(), x, y, characterPositionX, characterPositionY);
    }

    private Dictionary<(int, int), Tile> HighlightColumn(
        int x,
        int y,
        float minColumn,
        float maxColumn,
        GridHighlightRank highlightRank,
        Dictionary<(int, int), Tile> existingHighlightedTiles,
        Dictionary<(int, int), Tile> visitedTiles,
        int initialX,
        int initialY,
        int characterPositionX,
        int characterPositionY)
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
        if (sqrDistance > maxColumn * maxColumn)
        {
            return existingHighlightedTiles;
        }

        if (sqrDistance >= minColumn * minColumn)
        {
            existingHighlightedTiles.Add((x, y), tile);
            CreateHighlightObject(x, y, highlightRank);
        }
        visitedTiles.Add((x, y), tile);

        if (x == characterPositionX && y >= characterPositionY)
        {
            HighlightColumn(x, y + 1, minColumn, maxColumn, highlightRank, existingHighlightedTiles, visitedTiles, initialX, initialY, characterPositionX, characterPositionY); //Up
        }
        if (x == characterPositionX && y <= characterPositionY)
        {
            HighlightColumn(x, y - 1, minColumn, maxColumn, highlightRank, existingHighlightedTiles, visitedTiles, initialX, initialY, characterPositionX, characterPositionY); //Down
        }

        if (x >= characterPositionX && y == characterPositionY)
        {
            HighlightColumn(x + 1, y, minColumn, maxColumn, highlightRank, existingHighlightedTiles, visitedTiles, initialX, initialY, characterPositionX, characterPositionY); //Left
        }

        if (x <= characterPositionX && y == characterPositionY)
        {
            HighlightColumn(x - 1, y, minColumn, maxColumn, highlightRank, existingHighlightedTiles, visitedTiles, initialX, initialY, characterPositionX, characterPositionY); //Right
        }

        return existingHighlightedTiles;
    }

    /// <summary>
    /// Gets tile in a circle radius.
    /// </summary>
    /// <param name="x">X coordinate of the center of the circle.</param>
    /// <param name="y">Y coordinate of the center of the circle.</param>
    /// <param name="minRadius">Minimum radius of the circle. Tiles below this radius are not highlighted.</param>
    /// <param name="maxRadius">Maximum radius of the circle. Tiles above this radius are not highlighted.</param>
    /// <returns>Position-to-tile dictionary of the tiles.</returns>
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

    /// <summary>
    /// Gets column of tiles.
    /// </summary>
    /// <param name="x">Start x position of the column.</param>
    /// <param name="y">Start x position of the column.</param>
    /// <param name="minColumn">Minimum column height.</param>
    /// <param name="maxColumn">Maximum column height.</param>
    /// <param name="characterPositionX">X position of the character requesting the highlight.</param>
    /// <param name="characterPositionY">Y position of the character requesting the highlight.</param>
    /// <returns>Position-to-tile dictionary of the tiles.</returns>
    public Dictionary<(int, int), Tile> GetTilesInColumn(int x, int y, float minColumn, float maxColumn, int characterPositionX, int characterPositionY)
    {
        return GetTilesInColumn(x, y, minColumn, maxColumn, new Dictionary<(int, int), Tile>(), new Dictionary<(int, int), Tile>(), x, y, characterPositionX, characterPositionY);
    }

    private Dictionary<(int, int), Tile> GetTilesInColumn(
        int x,
        int y,
        float minColumn,
        float maxColumn,
        Dictionary<(int, int), Tile> tilesInColumn,
        Dictionary<(int, int), Tile> visitedTiles,
        int initialX,
        int initialY,
        int characterPositionX,
        int characterPositionY)
    {
        if (visitedTiles.ContainsKey((x, y)))
        {
            return tilesInColumn;
        }

        var tile = _grid.GetValue(x, y);
        if (tile == null)
        {
            return tilesInColumn;
        }

        var sqrDistance = Vector2.SqrMagnitude(new Vector2(initialX - x, initialY - y));
        if (sqrDistance > maxColumn * maxColumn)
        {
            return tilesInColumn;
        }

        if (sqrDistance >= minColumn * minColumn)
        {
            tilesInColumn.Add((x, y), tile);
        }
        visitedTiles.Add((x, y), tile);

        if (x == characterPositionX && y >= characterPositionY)
        {
            GetTilesInColumn(x, y + 1, minColumn, maxColumn, tilesInColumn, visitedTiles, initialX, initialY, characterPositionX, characterPositionY); //Up
        }
        if (x == characterPositionX && y <= characterPositionY)
        {
            GetTilesInColumn(x, y - 1, minColumn, maxColumn, tilesInColumn, visitedTiles, initialX, initialY, characterPositionX, characterPositionY); //Down
        }

        if (x >= characterPositionX && y == characterPositionY)
        {
            GetTilesInColumn(x + 1, y, minColumn, maxColumn, tilesInColumn, visitedTiles, initialX, initialY, characterPositionX, characterPositionY); //Left
        }

        if (x <= characterPositionX && y == characterPositionY)
        {
            GetTilesInColumn(x - 1, y, minColumn, maxColumn, tilesInColumn, visitedTiles, initialX, initialY, characterPositionX, characterPositionY); //Right
        }

        return tilesInColumn;
    }

    /// <summary>
    /// Highlights a dictionary of tiles.
    /// </summary>
    /// <param name="tiles">The position-to-tile dictionary of tiles to highlight.</param>
    /// <param name="highlightRank">The color rank of the highlight.</param>
    public void HighlightTiles(Dictionary<(int, int), Tile> tiles, GridHighlightRank highlightRank)
    {
        HighlightTiles(tiles.Values.ToList(), highlightRank);
    }

    /// <summary>
    /// Highlights a list of tiles.
    /// </summary>
    /// <param name="tiles">THe list of tiles to highlight.</param>
    /// <param name="highlightRank">The color rank of the highlight.</param>
    public void HighlightTiles(List<Tile> tiles, GridHighlightRank highlightRank)
    {
        foreach (var tile in tiles)
        {
            CreateHighlightObject(tile.GridX, tile.GridY, highlightRank);
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
                return VisualSettings.PrimaryHighlightPrefab;
            case GridHighlightRank.Secondary:
                return VisualSettings.SecondaryHighlightPrefab;
            case GridHighlightRank.Tertiary:
                return VisualSettings.TertiaryHighlightPrefab;
            case GridHighlightRank.Quaternary:
                return VisualSettings.QuaternaryHighlightPrefab;
            case GridHighlightRank.Quinary:
                return VisualSettings.QuinaryHighlightPrefab;
            default:
                return VisualSettings.PrimaryHighlightPrefab;
        }
    }

    /// <summary>
    /// Determines whether or not a tile contains a character.
    /// </summary>
    /// <param name="tile">The tile to check if there is a character.</param>
    /// <returns>Whether or not the given tile contains a character.</returns>
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

    /// <summary>
    /// Gets the tile that the mouse is hovering over.
    /// </summary>
    /// <returns>Tile that the mouse is hovering over.</returns>
    public Tile GetMouseHoverTile()
    {
        return _mouseHoverTile;
    }

    /// <summary>
    /// Gets tiles adjacent to the given tile.
    /// </summary>
    /// <param name="tile">The tile to get adjacent tiles of.</param>
    /// <param name="type">Type of tiles to get.</param>
    /// <param name="includeDiagonals">Whether or not to also get diagonal tiles.</param>
    /// <param name="excludedTiles">Tiles to exclude from the result.</param>
    /// <returns>List of tiles adjacent to the given tile.</returns>
    public List<Tile> GetAdjacentTiles(
        Tile tile,
        TileType type,
        bool includeDiagonals = false,
        Dictionary<(int, int), Tile> excludedTiles = null)
    {
        var adjacentTiles = _grid.GetAdjacentTiles(tile.GridX, tile.GridY, includeDiagonals);
        var filteredAdjacentTiles = new List<Tile>();

        foreach (var adjacentTile in adjacentTiles)
        {
            if (adjacentTile.Type == type && string.IsNullOrEmpty(adjacentTile.CharacterControllerId) &&
                (excludedTiles == null || !excludedTiles.ContainsKey((adjacentTile.GridX, adjacentTile.GridY))))
            {
                filteredAdjacentTiles.Add(adjacentTile);
            }
        }

        return filteredAdjacentTiles;
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
    Quaternary = 4,
    Quinary = 5
}
