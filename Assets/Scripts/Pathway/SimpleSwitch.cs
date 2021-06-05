using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSwitch : Switch
{
    [SerializeField] Sprite arrowWestDownSprite;
    [SerializeField] Sprite arrowNorthUpSprite;

    void OnMouseDown() => this.SwitchPath();

    public override void updateRendering(Direction pathDirection)
    {
        switch (pathDirection)
        {
            case Direction.North:
                spriteRenderer.sprite = arrowNorthUpSprite;
                break;
            case Direction.South:
                spriteRenderer.sprite = arrowWestDownSprite;
                spriteRenderer.flipX = true;
                break;
            case Direction.West:
                spriteRenderer.sprite = arrowWestDownSprite;
                break;
            case Direction.East:
                spriteRenderer.sprite = arrowNorthUpSprite;
                spriteRenderer.flipX = true;
                break;
        }
    }

}
