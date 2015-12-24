using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexGrid : MonoBehaviour, Grid  {

    public GameObject emptyTile;
    public GameObject optionTile;
    public bool generateFullGrid;
    public int gridSize;
    public enum GridType { SQUARE, HEXAGON };
    public GridType gridType = GridType.HEXAGON;

    List<GameObject> optionTiles;
    float tileSize;
    float offsetX, offsetY;
    Vector3 gridStartPosition;
    GameObject [,] grid;
    public bool isChoosing = false; //tracks if option tiles are active, think this is easier to read than just checking the optiontile array == 0.

    public void Start()
    {
        optionTiles = new List<GameObject>();

        float hexHeight = emptyTile.GetComponent<HexTile>().spriteRenderer.bounds.max.z * 2;
        Debug.Log(hexHeight);
        offsetY = hexHeight * (3.0f / 4.0f);
        offsetX = Mathf.Sqrt(3)/2 * hexHeight;

        gridStartPosition = transform.position;
        grid = new GameObject[gridSize, gridSize];      
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
        }
    }

    public Vector3 GridToWorld(int x, int y)
    {
        Vector3 position = Vector3.zero;
        float depth = gridStartPosition.y; //height/depth of start.

        if (y % 2 == 0)
        {
            position.x = x * offsetX;
            position.z = y * offsetY;
        }
        else
        {
            position.x = (x + 0.5f) * offsetX;
            position.z = y * offsetY;
        }
        position.y = depth;

        return position;
    }

    public void GenerateFullGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {               
                grid[i, j] = CreateTile(i, j);               
            }
        }
    }

    public void InitializeGridCenter()
    {
        //TODO:later add initial monster here; or other script doesn't matter much.
        int center = gridSize / 2; //int more or less count as rounded down and we use 0 based index so that gives u middle anyways 5 / 2 = 2  0, 1, 2, 3 ,4.  2 is middle.
        grid[center, center] = CreateTile(center, center);
    }

    public void PopulateGridPosition(int x, int y, GameObject g)
    {
        grid[x, y] = g;
    }

    public GameObject CreateTile(int x, int y)
    {
        Vector3 position = GridToWorld(x, y);
        GameObject temp = (GameObject)Instantiate(emptyTile, position, transform.rotation);
        HexTile hex = temp.GetComponent<HexTile>();
        //hextile needs this to easily access data from this script.
        hex.grid = this;
        hex.x = x;
        hex.y = y;
        grid[x, y] = temp;

        return temp;
    }

    public GameObject CreateOptionTile(int x, int y)
    {
        if (CheckNotOutOfBounds(x, y))
        {
            if (grid[x, y] == null)
            {
                Vector3 position = GridToWorld(x, y);
                GameObject temp = (GameObject)Instantiate(optionTile, position, transform.rotation);
                HexTile hex = temp.GetComponent<HexTile>();
                //hextile needs this to easily access data from this script.
                hex.grid = this;
                hex.x = x;
                hex.y = y;

                optionTiles.Add(temp);
                return temp;
            }
        }      
        return null;  
    }

    public void CreateTileOptionsAround(int x, int y)
    {
        Debug.Log("don");
        if (!isChoosing)
        {
            if (y % 2 == 0)
            {
                Debug.Log("don2");
                //control for -1 + 1, 0 + 1, -1 + 0, 1 + 0, 0 - 1, 0 - 1;
                CreateOptionTile(x - 1, y + 1);
                CreateOptionTile(x, y + 1);
                CreateOptionTile(x - 1, y);
                CreateOptionTile(x + 1, y);
                CreateOptionTile(x, y - 1);
                CreateOptionTile(x - 1, y - 1);
            }
            else
            {
                //control for 0 + 1, 1 + 1, -1 + 0, 1 + 0, 0 - 1, 1 - 1;
                CreateOptionTile(x, y + 1);
                CreateOptionTile(x + 1, y + 1);
                CreateOptionTile(x - 1, y);
                CreateOptionTile(x + 1, y);
                CreateOptionTile(x, y - 1);
                CreateOptionTile(x + 1, y - 1);
            }
            isChoosing = !isChoosing;
        }
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

    public bool CheckNotOutOfBounds(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridSize && y < gridSize)
        {
            return true;
        }

        return false;
    }

}
