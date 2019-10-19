﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * Does sprite placement and highlighting for placing traps.
 */
public class PlacementUIManager : MonoBehaviour
{
    // a prefab that will be instantiated as an object to 
    [SerializeField] private GameObject spriteHolder;
    // parent object for grid sprites
    [SerializeField] private GameObject spriteParent;
    // converts from screen to grid space
    [SerializeField] private ToGridSpaceConverters gSpace;
    // tracks objects on the grid
    [SerializeField] private ObjectGrid oGrid;

    /**
     * Adds a sprite onto the tile at the given position
     */
    public GameObject AddSpriteOnTile(Sprite toAdd, Vector3 screenPos, float alpha)
    {
        GameObject spriteObj = GameObject.Instantiate(spriteHolder, gSpace.SSToGPos(screenPos), Quaternion.identity, spriteParent.transform);
        spriteObj.GetComponent<SpriteRenderer>().sprite = toAdd;
        spriteObj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
        return spriteObj;
    }

    /**
     * Colors the trap at the given position if one exists. Returns that trap's SpriteRenderer whcih should be passed into UnhighlightTrap.
     */
    public SpriteRenderer HighlightTrap(Vector3 screenPos, Color color) 
    {
        if (oGrid.CheckCell(screenPos))
        {
            SpriteRenderer spriteRenderer = oGrid.GetCellObject(screenPos).GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
            return spriteRenderer;
        }
        return null;
    }

    /**
     * Sets a traps spriteRenderer back to its original white color.
     */
    public void UnhighlightTrap(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

}