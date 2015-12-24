using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HexagonGrid : MonoBehaviour, Grid
{
    public int gridSize;
    public GameObject emptyTile;
    public GameObject optionTile;
    public bool generateFullGrid;

    List<GameObject> optionTiles;
    float tileSize;
    bool isChoosing = false;
    float offsetX, offsetY;

    Dictionary<Coordinate, GameObject> grid;

    public void Start()
    {
        grid = new Dictionary<Coordinate, GameObject>();
        optionTiles = new List<GameObject>();

        tileSize = emptyTile.GetComponent<HexTile>().spriteRenderer.bounds.max.z;
        Debug.Log(tileSize);
        

        GenerateGrid();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && isChoosing)
        {
            ClearAllOptionTiles();
        }
    }

    public void GenerateGrid()
    {
        if (generateFullGrid)
        {
            GenerateFullGrid();
        }
        else
        {
            InitializeGridCenter();
            Debug.Log("yes");
        }
    }

    public bool CheckNotOutOfBounds(int q, int r)
    {
        if (Mathf.Abs(q) <= gridSize && Mathf.Abs(r) <= gridSize)
        {
            return true;
        }
        return false;
    }

    public void ClearAllOptionTiles()
    {
        for (int i = 0; i < optionTiles.Count; i++)
        {
            Destroy(optionTiles[i]);
        }
        isChoosing = !isChoosing;
        optionTiles.Clear();
        Debug.Log(optionTiles.Count);
    }

    public GameObject CreateOptionTile(int q, int r)
    {
        return null;
    }

    public GameObject CreateTile(int q, int r)
    {
        Vector3 position = GridToWorld(q, r);
        GameObject temp = (GameObject)Instantiate(emptyTile, position, transform.rotation);
        HexTile hex = temp.GetComponent<HexTile>();
        //hextile needs this to easily access data from this script.
        hex.grid = this;
        Debug.Log(hex.grid);
        hex.x = q;
        hex.y = r;
        Coordinate c = new Coordinate(q, r);
        Debug.Log(c.GetHashCode());
        grid.Add(c, temp);

        return temp;
    }

    public void CreateTileOptionsAround(int q, int r)
    {

    }

    public void GenerateFullGrid()
    {
        for (int q = 0 - gridSize; q < gridSize + 1 ; q++)
        {
            for (int r = 0 - gridSize; r < gridSize + 1; r++)
            {
                if (true)
                {
                    CreateTile(q, r);
                }
            }
        }
    }

    public Vector3 GridToWorld(int q, int r)
    {
        Vector3 position = Vector3.zero;
        float qf = (float)q;
        float rf = (float)r;
        position.x = tileSize * Mathf.Sqrt(3) * (qf + rf / 2);
        position.z = -tileSize * 1.5f * rf;
        position.y = transform.position.y;

        return position;
    }

    public void InitializeGridCenter()
    {
        CreateTile(0, 0);
    }

    public void PopulateGridPosition(int q, int r, GameObject g)
    {
    }

    public GameObject GetTile(int q, int r)
    {
        Coordinate c = new Coordinate(q, r);
        Debug.Log(c.GetHashCode());
        if (grid.ContainsKey(c))
        {
            Debug.Log("Key " + c.GetHashCode() + "is in dictionary");
            return grid[c];
        }
        return null;
    }
}
