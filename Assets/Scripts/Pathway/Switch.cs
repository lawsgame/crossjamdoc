using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pathfinder;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] Sprite arrowDown;
    [SerializeField] Sprite arrowUp;

    public int switchCellX;
    public int switchCellY;

    private Pathfinder pathfinder;
    private Node switchNode;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pathfinder = gameManager.GetComponent<Pathfinder>();
        Vector3Int nodepos = new Vector3Int(switchCellX, switchCellY, Pathfinder.TILE_Z);
        switchNode = pathfinder.FindNode(nodepos);
        if (switchNode == null)
        {
            Debug.LogError(string.Format("No switch node found at {0}", nodepos));
        }
        else
        {
            updateSprite();
        }
    }


    void OnMouseDown()
    {
        Debug.Log("clicked");
        SwitchPath();
    }

    public void SwitchPath()
    {
        switchNode.Switch();
        updateSprite();

    }

    public void updateSprite()
    {
        Direction direction = switchNode.GetDirectionTowardsNext();
        if (direction == Direction.West)
        {
            spriteRenderer.sprite = arrowDown;
        }
        else if (direction == Direction.South)
        {
            spriteRenderer.sprite = arrowDown;
            spriteRenderer.flipX = true;
        }
        else if (direction == Direction.North)
        {
            spriteRenderer.sprite = arrowUp;
        }
        else if (direction == Direction.East)
        {
            spriteRenderer.sprite = arrowUp;
            spriteRenderer.flipX = true;
        }
    }

}
