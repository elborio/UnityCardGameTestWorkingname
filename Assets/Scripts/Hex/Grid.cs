using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Grid 
{
    Vector3 GridToWorld(int x, int y);
    void GenerateFullGrid();
    void InitializeGridCenter();
    void PopulateGridPosition(int x, int y, GameObject g);
    GameObject CreateTile(int x, int y);
    GameObject CreateOptionTile(int x, int y);
    void CreateTileOptionsAround(int x, int y);
    void ClearAllOptionTiles();
    bool CheckNotOutOfBounds(int x, int y);
}
