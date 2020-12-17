using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridPointCalculator : MonoBehaviour
{
    public TextLabel TextLabelPrefab;
    public bool ShowPreview;

    private Dictionary<(int, int), TextLabel> _tilePointsLabels = new Dictionary<(int, int), TextLabel>();

    private void Update()
    {
        foreach (var textLabel in _tilePointsLabels.Values)
        {
            textLabel.gameObject.SetActive(ShowPreview);
        }
    }

    public void CalculateGridPoints()
    {
        var grid = TileGridController.Instance.GetGrid();
        for (var x = 0; x < grid.GetGrid().GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetGrid().GetLength(1); y++)
            {
                var tile = grid.GetValue(x, y);
                tile.Points = Random.Range(1, 33);
            }
        }
    }

    public void PreviewGridPoints()
    {
        foreach (var textLabel in _tilePointsLabels.Values)
        {
            Destroy(textLabel);
        }
        _tilePointsLabels = new Dictionary<(int, int), TextLabel>();

        var grid = TileGridController.Instance.GetGrid();
        for (var x = 0; x < grid.GetGrid().GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetGrid().GetLength(1); y++)
            {
                var tile = grid.GetValue(x, y);
                var textLabel = Instantiate(TextLabelPrefab, TileGridController.Instance.transform);
                textLabel.transform.position = grid.GetWorldPositionCentered(x, y);
                textLabel.SetText(tile.Points.ToString());
                _tilePointsLabels.Add((x, y), textLabel);
            }
        }
    }

    public void UpdatePreview()
    {
        var grid = TileGridController.Instance.GetGrid();
        foreach (var textLabelKeyVal in _tilePointsLabels)
        {
            var tile = grid.GetValue(textLabelKeyVal.Key.Item1, textLabelKeyVal.Key.Item2);
            textLabelKeyVal.Value.SetText(tile.Points.ToString());
        }
    }
}
