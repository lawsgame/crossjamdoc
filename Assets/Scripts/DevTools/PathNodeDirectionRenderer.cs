using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Pathfinder;

public class PathNodeDirectionRenderer : MonoBehaviour
{
    public Sprite pathArrowWestSprite;
    public Sprite pathArrowNorthSprite;
    public Sprite intersectionSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Render(Node node, Tilemap worldMap)
    {
        if(node != null)
        {
            transform.position = node.GetWorldPos(worldMap);
            if (node.IsIntersection())
            {
                spriteRenderer.sprite = intersectionSprite;
            }
            else
            {
                Direction direction = node.GetDirectionTowardsNext();
                if (direction == Direction.North)
                {
                    spriteRenderer.sprite = pathArrowNorthSprite;
                }
                else if (direction == Direction.South)
                {
                    
                    spriteRenderer.sprite = pathArrowWestSprite;
                    spriteRenderer.flipX = true;
                }
                else if (direction == Direction.East)
                {
                    spriteRenderer.sprite = pathArrowNorthSprite;
                    spriteRenderer.flipX = true;
                }
                else if (direction == Direction.West)
                {
                    spriteRenderer.sprite = pathArrowWestSprite;
                   
                }
        }
        }
       
    }
}
