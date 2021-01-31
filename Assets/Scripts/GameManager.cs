using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public TextMeshProUGUI finalPoopNumText;
    public TextMeshProUGUI currentScansLeftText;
    public TextMeshProUGUI currentScoopsLeftText;
    public TextMeshProUGUI noScansLeftText;
    public TextMeshProUGUI noScoopsLeftText;
    public GameObject finalTextBackground;

    private int currentPoopNum;

    private bool hasGameStarted;
    public int scansLeft;
    public int scoopsLeft;

    public TileManager tileManager;
    // Start is called before the first frame update
    void Start()
    {
        // initialize to the start scene
        gameState = GameState.START;
        scansLeft = 6;
        scoopsLeft = 3;
        currentScansLeftText.SetText(scansLeft.ToString());
        currentScoopsLeftText.SetText(scoopsLeft.ToString());
        foreach (GameObject obj in startStateObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in resourceGatheringObjects)
        {
            obj.SetActive(false);
        }

        hasGameStarted = false;

        // initialize the game itself to start in extract mode
        resourceGatheringMode = ResourceGatheringMode.EXTRACT;

        // in case for some reason the current number is not 0
        currentPoopNumText.SetText(currentPoopNum.ToString());

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

            if (!hasGameStarted)
            {
                tileManager.StartResourceGatheringMode();
            }

        }
    }

    public void RestartEverything()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void DecrementScans()
    {
        scansLeft--;
        currentScansLeftText.SetText(scansLeft.ToString());
        if (scansLeft == 0)
        {
            noScansLeftText.gameObject.SetActive(true);
        }
    }

    public void DecrementScoops()
    {
        scoopsLeft--;
        currentScoopsLeftText.SetText(scoopsLeft.ToString());
        if (scoopsLeft == 0)
        {
            noScoopsLeftText.gameObject.SetActive(true);
            finalTextBackground.SetActive(true);
            finalPoopNumText.SetText(currentPoopNum.ToString());
            
        }
    }

    public void AddResources(ResourceAmount amount)
    {

        if (amount == ResourceAmount.MAX)
        {
            // add loads
            AddPoop(1000);
        }
        else if (amount == ResourceAmount.HALF)
        {
            // add half of that
            AddPoop(500);
        }
        else if (amount == ResourceAmount.QUARTER)
        {
            // add half of THAT
            AddPoop(250);
        }
    }
}
