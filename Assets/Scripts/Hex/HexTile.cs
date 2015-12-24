using UnityEngine;
using System.Collections;

public class HexTile : MonoBehaviour {

    public int x, y;
    public Grid grid;
    public Types.tileType type;
    public SpriteRenderer spriteRenderer;

    Color startColor;

    public HexTile()
    {
        type = Types.tileType.EMPTY;
    }
	
    public void Start()
    {
        startColor = spriteRenderer.color;
    }

    public virtual void OnMouseOver()
    {
        //if (type == Types.tileType.EMPTY)
        //{
            SetColor(Color.red);
        Debug.Log("Coordinates: " + x + ", " + y);
        //}
    }

    public virtual void OnMouseExit()
    {
        SetColor(startColor);
    }

    public virtual void OnMouseDown()
    {
        if (type == Types.tileType.EMPTY)
        {
            CreateSurroundingOptions();
        }
        else if (type == Types.tileType.OPTION)
        {
            PresentOptions(x, y);
            //for now just settile to empty tile.
            grid.CreateTile(x, y);
            grid.ClearAllOptionTiles();
            Destroy(gameObject);
        }

        
    }

    public void SetColor(Color c)
    {
        spriteRenderer.color = c;
    }

    public void CreateSurroundingOptions()
    {
        grid.CreateTileOptionsAround(x, y);
    }

    public void PresentOptions(int x, int y)
    {

    }
}
