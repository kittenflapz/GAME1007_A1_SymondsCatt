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
    public List<List<GameObject>> tileMap;
    public Vector3 startTilePosition;
    public Vector3 nextTilePosition;


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


    // ty @ https://answers.unity.com/questions/409348/how-to-replace-2d-array-with-list.html
    public void InstantiateTiles()
    {
        tileMap = new List<List<GameObject>>();
        // rows
        for (int i = 0; i < 32; i++)
        {
            tileMap.Add(new List<GameObject>());
            // columns
            for (int j = 0; j < 32; j++)
            {
                GameObject go = Instantiate(defaultTile, nextTilePosition, Quaternion.identity);
                nextTilePosition = new Vector3(nextTilePosition.x + 0.25f, nextTilePosition.y, nextTilePosition.z);
                go.transform.parent = this.transform;
                tileMap[i].Add(go);
            }
            nextTilePosition = new Vector3(startTilePosition.x, nextTilePosition.y + 0.25f, nextTilePosition.z);
        }

    }

    public void PopulateTilesWithResources()
    {
        // first, randomly pick some tiles to add max resources to
        int randomNumMaxResourceTiles = Random.Range(7, 10);

        for (int i = 0; i < randomNumMaxResourceTiles; i++)
        {
            int randomRow = Random.Range(0, 32);
            int randomColumn = Random.Range(0, 32);

            Debug.Log("updating " + randomRow + " " + randomColumn + " to max");

            Tile currentTile = tileMap[randomRow][randomColumn].GetComponent<Tile>();

            // add max resources to this tile
            currentTile.resourceAmount = ResourceAmount.MAX;
            currentTile.UpdateColor();
            Debug.Log(currentTile.resourceAmount);
            // send this info to function to populate surrounding tiles, deal with that logic there
            PopulateSurroundingTiles(randomRow, randomColumn);
        }

        tileMap[0][0].GetComponent<Tile>().resourceAmount = ResourceAmount.MAX;
        tileMap[0][0].GetComponent<Tile>().UpdateColor();
    }


    // so unnecessarily long
    public void PopulateSurroundingTiles(int rowOfCentreTile, int colOfCentreTile)
    {
        // lazy
        Tile temp = tileMap[rowOfCentreTile][colOfCentreTile].GetComponent<Tile>();

        // immediate left
        // check it exists
        if (tileMap[rowOfCentreTile][colOfCentreTile -1])
        {
            // set it to half resource
            temp = (tileMap[rowOfCentreTile][colOfCentreTile - 1].GetComponent<Tile>());
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }

        // immediate right
        if (tileMap[rowOfCentreTile][colOfCentreTile + 1])
        {
            // set it to half resource
            temp = (tileMap[rowOfCentreTile][colOfCentreTile + 1].GetComponent<Tile>());
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }

        // immediate top
        if (tileMap[rowOfCentreTile + 1][colOfCentreTile])
        {
            // set it to half resource
            temp = (tileMap[rowOfCentreTile + 1][colOfCentreTile].GetComponent<Tile>());
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }

        // immediate bottom
        if (tileMap[rowOfCentreTile - 1][colOfCentreTile])
        {
            // set it to half resource
            temp = (tileMap[rowOfCentreTile - 1][colOfCentreTile].GetComponent<Tile>());
            temp.resourceAmount = ResourceAmount.HALF;
            temp.UpdateColor();
        }
    }


    // why does this only work after a delay? I don't know.
    IEnumerator WaitAndPopulateTiles()
    {
        yield return new WaitForSeconds(0.5f);
        PopulateTilesWithResources();
    }
}
