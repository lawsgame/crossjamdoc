using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pathfinder;

public abstract class Switch : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    
    public int switchCellX;
    public int switchCellY;

    protected SpriteRenderer spriteRenderer;

    private Pathfinder pathfinder;
    private Node switchNode;
    private bool actived = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pathfinder = gameManager.GetComponent<Pathfinder>();

        Vector3Int nodepos = new Vector3Int(switchCellX, switchCellY, Pathfinder.TILE_Z);
        switchNode = pathfinder.FindNode(nodepos);
        if (switchNode == null)
            Debug.LogWarning(string.Format("No intersection node found at {0}!!", nodepos));
        else if(!switchNode.IsIntersection())
            Debug.LogWarning(string.Format("The node found at {0} is NOT an intersection!!", nodepos));
        else
        {
            Debug.Log(string.Format("Switch set up at {0}", nodepos));
            updateRendering(switchNode.GetDirectionTowardsNext());
            actived = true;
        }
            
    }

    public void SwitchPath()
    {
        if (actived)
        {
            switchNode.Switch();
            Direction direction = switchNode.GetDirectionTowardsNext();
            updateRendering(direction);
        }
    }

    public abstract void updateRendering(Direction pathDirection);

}
