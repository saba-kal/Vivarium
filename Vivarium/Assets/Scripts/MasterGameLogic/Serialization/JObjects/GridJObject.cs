using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GridJObject
{
    public int Width;
    public int Height;
    public float TileSize;
    public float OriginX;
    public float OriginY;
    public float OriginZ;
    public List<GridRowJObject> Rows;
}
