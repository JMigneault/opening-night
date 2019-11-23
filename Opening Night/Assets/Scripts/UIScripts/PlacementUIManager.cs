using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * Does sprite placement and highlighting for placing traps.
 */
public class PlacementUIManager : MonoBehaviour
{
    //// a prefab that will be instantiated as an object to 
    [SerializeField] private ToGridSpaceConverters gSpace;
    // tracks objects on the grid
    [SerializeField] private ObjectGrid oGrid;

    /**
     * Adds a sprite onto the tile at the given position
     */
    public void MoveSpriteToTile(GameObject spriteObj, Vector3 screenPos)
    {
        spriteObj.transform.position = gSpace.SSToGPos(screenPos);
    }

    /**
     * Colors the trap at the given position if one exists. Returns that trap's SpriteRenderer whcih should be passed into UnhighlightTrap.
     */
    public AbstractTrap HighlightTrap(Vector3 screenPos, Color color) 
    {
        if (oGrid.CheckCell(screenPos))
        {
            AbstractTrap trap = (AbstractTrap) oGrid.GetCellObject(screenPos);
            trap.SetColor(color);
            return trap;
        }
        return null;
    }

    /**
     * Sets a traps spriteRenderer back to its original white color.
     */
    public void UnhighlightTrap(AbstractTrap trap)
    {
        if (trap != null)
        {
            trap.SetColor(Color.white);
        }
    }

}
