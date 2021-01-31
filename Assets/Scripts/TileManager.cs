using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceAmount
{
    MAX,
    HALF,
    QUARTER,
    NONE
}

public class TileManager : MonoBehaviour
{
    public GameObject defaultTile;
    //public List<List<GameObject>> tileMap;
    public GameObject[,] tileMap;
    public Vector3 startTilePosition;
    public Vector3 nextTilePosition;

    private const int tileMapLength = 32;


    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartResourceGatheringMode()
    {
        startTilePosition = new Vector3(-4f, -3.5f, 1f);
        nextTilePosition = startTilePosition;
        InstantiateTiles();
        StartCoroutine(WaitAndPopulateTiles());
    }

    public void InstantiateTiles()
    {
        tileMap = new GameObject[tileMapLength, tileMapLength];
        // rows
        for (int i = 0; i < tileMapLength; i++)
        {
            // columns
            for (int j = 0; j < tileMapLength; j++)
            {
                GameObject go = Instantiate(defaultTile, nextTilePosition, Quaternion.identity);
                nextTilePosition = new Vector3(nextTilePosition.x + 0.25f, nextTilePosition.y, nextTilePosition.z);
                go.transform.parent = this.transform;
                tileMap[i, j] = go;
                go.GetComponent<Tile>().SetColumnAndRow(i, j);
            }
            nextTilePosition = new Vector3(startTilePosition.x, nextTilePosition.y + 0.25f, nextTilePosition.z);
        }

    }

    public void PopulateTilesWithResources()
    {
        // first, randomly pick some tiles to add max resources to
        int randomNumMaxResourceTiles = Random.Range(20, 25);

        for (int i = 0; i < randomNumMaxResourceTiles; i++)
        {
            int randomRow = Random.Range(0, 32);
            int randomColumn = Random.Range(0, 32);

            Tile currentTile = tileMap[randomRow, randomColumn].GetComponent<Tile>();

            // add max resources to this tile
            currentTile.resourceAmount = ResourceAmount.MAX;
            currentTile.UpdateColor();
            Debug.Log(currentTile.resourceAmount);
            // send this info to function to populate surrounding tiles, deal with that logic there
            PopulateSurroundingTiles(randomRow, randomColumn);
        }
    }


    // so unnecessarily long. I determined that it would take more time to think of and plan a better way to do this and time is precious so we're doing ti this way
    public void PopulateSurroundingTiles(int rowOfCentreTile, int colOfCentreTile)
    {
        // lazy
        Tile temp = tileMap[rowOfCentreTile, colOfCentreTile].GetComponent<Tile>();

        // checking it exists is hard so literally going to check in hard code if its in the bounds that i know exist 

        // HALF RESOURCE (directly around the center tile)
        // immediate left
        if (colOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile, colOfCentreTile - 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }

        // immediate right
        if (colOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile, colOfCentreTile + 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }

        // immediate top
        if (rowOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile + 1, colOfCentreTile].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }

        // immediate bottom
        if (rowOfCentreTile > 0)
        {
          temp = tileMap[rowOfCentreTile - 1, colOfCentreTile].GetComponent<Tile>();
          temp.resourceAmount = ResourceAmount.HALF;
          temp.UpdateColor();
        }

        // top right
        if (rowOfCentreTile < (tileMapLength - 1) && colOfCentreTile < (tileMapLength - 1))
        {
         temp = tileMap[rowOfCentreTile + 1, colOfCentreTile + 1].GetComponent<Tile>();
         temp.resourceAmount = ResourceAmount.HALF;
         temp.UpdateColor();
        }

        // top left
        if (rowOfCentreTile < tileMapLength - 1 && colOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile + 1, colOfCentreTile - 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }


        // bottom left
        if (rowOfCentreTile > 0 && colOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile - 1, colOfCentreTile - 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }


        // bottom right
        if (rowOfCentreTile > 0 && colOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile - 1, colOfCentreTile + 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }

        // far left
        if (colOfCentreTile > 1)
        {
            temp = tileMap[rowOfCentreTile, colOfCentreTile - 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // trotsky (left and up a bit)
        if (colOfCentreTile > 1 && rowOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile + 1, colOfCentreTile - 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // stalin (left and up a lot)
        if (colOfCentreTile > 1 && rowOfCentreTile < tileMapLength - 2)
        {
            temp = tileMap[rowOfCentreTile + 2, colOfCentreTile - 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // sjws (left and down a bit)
        if (colOfCentreTile > 1 && rowOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile - 1, colOfCentreTile - 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // really bad sjws (left and down a lot)
        if (colOfCentreTile > 1 && rowOfCentreTile > 1)
        {
            temp = tileMap[rowOfCentreTile - 2, colOfCentreTile - 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // far right

        if (colOfCentreTile < tileMapLength - 2)
        {
            temp = tileMap[rowOfCentreTile, colOfCentreTile + 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // right and up a bit

        if (colOfCentreTile < tileMapLength - 2 && rowOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile + 1, colOfCentreTile + 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // right and up a lot
        if (colOfCentreTile < tileMapLength - 2 && rowOfCentreTile < tileMapLength - 2)
        {
            temp = tileMap[rowOfCentreTile + 2, colOfCentreTile + 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // right and down a bit

        if (colOfCentreTile < tileMapLength - 2 && rowOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile - 1, colOfCentreTile + 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }


        // right and down a lot

        if (colOfCentreTile < tileMapLength - 2 && rowOfCentreTile > 1)
        {
            temp = tileMap[rowOfCentreTile - 2, colOfCentreTile + 2].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }


        // far top
        if (rowOfCentreTile < tileMapLength - 2)
        {
            temp = tileMap[rowOfCentreTile + 2, colOfCentreTile].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }


        // far top and left a bit
        if (rowOfCentreTile < tileMapLength - 2 && colOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile + 2, colOfCentreTile - 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // far top and right a bit
        if (rowOfCentreTile < tileMapLength - 2 && colOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile + 2, colOfCentreTile + 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // far bottom
        if (rowOfCentreTile > 1)
        {
            temp = tileMap[rowOfCentreTile - 2, colOfCentreTile].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // bottom and left a bit
        if (rowOfCentreTile > 1&& colOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile - 2, colOfCentreTile - 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

        // bottom and right a bit
        if (rowOfCentreTile > 1 && colOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile - 2, colOfCentreTile + 1].GetComponent<Tile>();
            temp.resourceAmount = ResourceAmount.QUARTER;
            temp.UpdateColor();
        }

    }


    public void ScanTileAndSurroundingTiles(Tile centreTile)
    {
        centreTile.ScanMe();

        int rowOfCentreTile = centreTile.row;
        int colOfCentreTile = centreTile.column;

        Tile temp = new Tile();

        if (colOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile, colOfCentreTile - 1].GetComponent<Tile>();
            temp.ScanMe();
        }

        // immediate right
        if (colOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile, colOfCentreTile + 1].GetComponent<Tile>();
            temp.ScanMe();
        }

        // immediate top
        if (rowOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile + 1, colOfCentreTile].GetComponent<Tile>();
            temp.ScanMe();
        }

        // immediate bottom
        if (rowOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile - 1, colOfCentreTile].GetComponent<Tile>();
            temp.ScanMe();
        }

        // top right
        if (rowOfCentreTile < (tileMapLength - 1) && colOfCentreTile < (tileMapLength - 1))
        {
            temp = tileMap[rowOfCentreTile + 1, colOfCentreTile + 1].GetComponent<Tile>();
            temp.ScanMe();
        }

        // top left
        if (rowOfCentreTile < tileMapLength - 1 && colOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile + 1, colOfCentreTile - 1].GetComponent<Tile>();
            temp.ScanMe();
        }


        // bottom left
        if (rowOfCentreTile > 0 && colOfCentreTile > 0)
        {
            temp = tileMap[rowOfCentreTile - 1, colOfCentreTile - 1].GetComponent<Tile>();
            temp.ScanMe();
        }


        // bottom right
        if (rowOfCentreTile > 0 && colOfCentreTile < tileMapLength - 1)
        {
            temp = tileMap[rowOfCentreTile - 1, colOfCentreTile + 1].GetComponent<Tile>();
            temp.ScanMe();
        }

    }


    // why does this only work after a delay? I don't know.
    IEnumerator WaitAndPopulateTiles()
    {
        yield return new WaitForSeconds(0.5f);
        PopulateTilesWithResources();
    }

}


