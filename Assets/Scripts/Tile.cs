using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameManager gameManager;
    public TileManager tileManager;
    public ResourceAmount resourceAmount = ResourceAmount.NONE;
    public SpriteRenderer spriteRenderer;
    public bool visible;


    // i'm setting these during tilemap generation so as not to have to write a helper function to get coordinates in a 2d array
    // it's inefficient probably
    public int column;
    public int row;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        tileManager = FindObjectOfType<TileManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       // resourceAmount = ResourceAmount.NONE;
        UpdateColor();
        resourceAmount = ResourceAmount.NONE;
        visible = false;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void OnMouseUp()
    {
        //  When the player clicks a tile in Scan Mode, it will display the resource underneath as well as those tiles that immediately surround it, for a total of 9 tiles.

        if (gameManager.resourceGatheringMode == ResourceGatheringMode.SCAN)
        {
            if (gameManager.scansLeft > 0)
            {
                tileManager.ScanTileAndSurroundingTiles(this);
                gameManager.DecrementScans();
            }
        }
        else
        {
            if (gameManager.scoopsLeft > 0)
            {
                tileManager.SuckResources(this);
                gameManager.AddResources(resourceAmount);
                gameManager.DecrementScoops();
            }
        }
    }

    public void UpdateColor()
    {
        if (visible)
        {
            switch (resourceAmount)
            {
                case ResourceAmount.MAX:
                    spriteRenderer.color = Color.black;
                        
                    break;
                case ResourceAmount.HALF:
                    spriteRenderer.color = Color.grey;
                    break;
                case ResourceAmount.QUARTER:
                    spriteRenderer.color = Color.green;
                    break;
                case ResourceAmount.NONE:
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                    break;
            }
        }
        else
        {
            spriteRenderer.color = new Color(255, 255, 255, 255);
        }
    }

    public void SetColumnAndRow(int col, int ro)
    {
        column = col;
        row = ro;
    }

    public void ScanMe()
    {
        visible = true;
        UpdateColor();
    }

    public void DegradeResource()
    {
        if (resourceAmount == ResourceAmount.MAX)
        {
            resourceAmount = ResourceAmount.HALF;
        }
        else if (resourceAmount == ResourceAmount.HALF)
        {
            resourceAmount = ResourceAmount.QUARTER;
        }
        else if (resourceAmount == ResourceAmount.QUARTER)
        {
            resourceAmount = ResourceAmount.NONE;
        }

        UpdateColor();
    }
}
