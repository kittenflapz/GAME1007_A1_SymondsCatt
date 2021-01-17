using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState {
    START,
    RESOURCE_GATHERING
}

public enum ResourceGatheringMode
{
    SCAN,
    EXTRACT
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public ResourceGatheringMode resourceGatheringMode;

    // todo: do this with tags instead of manually dragging everything in like a caveman
    public List<GameObject> startStateObjects;
    public List<GameObject> resourceGatheringObjects;

    // resource gathering mode stuff
    public TextMeshProUGUI currentModeText;

    public TextMeshProUGUI currentPoopNumText;
    private int currentPoopNum;
    // Start is called before the first frame update
    void Start()
    {
        // initialize to the start scene
        gameState = GameState.START;
        foreach (GameObject obj in startStateObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in resourceGatheringObjects)
        {
            obj.SetActive(false);
        }

        // initialize the game itself to start in extract mode
        resourceGatheringMode = ResourceGatheringMode.EXTRACT;

        // in case for some reason the current number is not 0
        currentPoopNumText.SetText(currentPoopNum.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // kinda lomg
    public void ToggleGameState()
    {
        if (gameState == GameState.RESOURCE_GATHERING)
        {
            gameState = GameState.START;
            foreach (GameObject obj in startStateObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in resourceGatheringObjects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            gameState = GameState.RESOURCE_GATHERING;
            foreach (GameObject obj in startStateObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in resourceGatheringObjects)
            {
                obj.SetActive(true);
            }
        }
    }


    public void ToggleResourceGatheringMode()
    {
        if (resourceGatheringMode == ResourceGatheringMode.SCAN)
        {
            resourceGatheringMode = ResourceGatheringMode.EXTRACT;
            currentModeText.SetText("Extract");
        }
        else
        {
            resourceGatheringMode = ResourceGatheringMode.SCAN;
            currentModeText.SetText("Scan");
        }
    }

    public void AddPoop(int numPoops)
    {
      
        currentPoopNum += numPoops;
        currentPoopNumText.SetText(currentPoopNum.ToString());
    }
}
