using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject defaultTile;
    public List<List<Tile>> tileMap;
    public Vector3 startTilePosition;
    public Vector3 nextTilePosition;


    // Start is called before the first frame update
    void Start()
    {
        startTilePosition = new Vector3(-4f, -3.5f, 1f);
        nextTilePosition = startTilePosition; 
        InstantiateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // ty @ https://answers.unity.com/questions/409348/how-to-replace-2d-array-with-list.html
    public void InstantiateTiles()
    {
        tileMap = new List<List<Tile>>();
        // rows
        for (int i = 0; i < 32; i++)
        {
            tileMap.Add(new List<Tile>());
            // columns
            for (int j = 0; j < 32; j++)
            {
                GameObject go = Instantiate(defaultTile, nextTilePosition, Quaternion.identity);
                nextTilePosition = new Vector3(nextTilePosition.x + 0.25f, nextTilePosition.y, nextTilePosition.z);
                go.transform.parent = this.transform;
                tileMap[i].Add(go.GetComponent<Tile>());
            }
            nextTilePosition = new Vector3(startTilePosition.x, nextTilePosition.y + 0.25f, nextTilePosition.z);
        }
    }
}
