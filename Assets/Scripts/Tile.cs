using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameManager gameManager;
    public ResourceAmount resourceAmount = ResourceAmount.NONE;
    public SpriteRenderer spriteRenderer;
    public bool visible;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       // resourceAmount = ResourceAmount.NONE;
        UpdateColor();
        resourceAmount = ResourceAmount.NONE;
        visible = true;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void OnMouseUp()
    {
        gameManager.AddPoop(1);
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
}
