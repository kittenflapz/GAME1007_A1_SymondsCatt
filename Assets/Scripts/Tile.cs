using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameManager gameManager;
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
            visible = true;
            UpdateColor();
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
}
