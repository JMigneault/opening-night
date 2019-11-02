using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    Barrier = 0,
    SlowMovement = 1,
    StopMovement = 2,
    ForceMovement = 3
}


// sample client code for ObjectGrid
public class TrapPlacer : MonoBehaviour
{
    [SerializeField] ObjectGrid objectGrid;
    public AbstractTrap[] traps;
    private TrapType currentTrap;

    [SerializeField] PlacementUIManager placementUI;

    private SpriteRenderer highlightedTrapSR;
    private GameObject currentPlacementSprite;
    [SerializeField] private Color highlightColor;
    [SerializeField] private float highlightAlpha;


    //Start called once
    private void Start()
    {
        currentTrap = TrapType.Barrier;
        highlightedTrapSR = null;
    }

    //method for setting current trap
    private AbstractTrap GetTrap(TrapType trapType)
    {
        foreach (AbstractTrap trap in traps)
        {
            if (trap.GetTrapType() == trapType)
            {
                return trap;
            }
        }
        Debug.LogError("Get Trap: No matching trap found.");
        return null;
    }

    public void ChangeTrap(TrapType trapType)
    {
        this.currentTrap = trapType;
    }

    public void HighlightTrap(Vector2 screenPos)
    {
        SpriteRenderer nextSR = placementUI.HighlightTrap(screenPos, highlightColor);
        if (nextSR != highlightedTrapSR)
        {
            placementUI.UnhighlightTrap(highlightedTrapSR);
            highlightedTrapSR = nextSR;
        }
    }

    public void HoverTile(Vector2 screenPos)
    {
        if (!objectGrid.CheckCell(screenPos))
        {
            // todo: improve this; shouldn't have to create a new gameobject every frame
            currentPlacementSprite = placementUI.AddSpriteOnTile(GetTrap(currentTrap).GetComponent<Sprite>(), screenPos, highlightAlpha);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Selecting type of trap
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTrap(TrapType.Barrier);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            ChangeTrap(TrapType.SlowMovement);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeTrap(TrapType.StopMovement);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeTrap(TrapType.ForceMovement);
        }

        Vector2 mp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // HoverTile(mp);

        // gets input
        if (Input.GetMouseButton(0))
        {
            // checking for other objects in cell
            if (!objectGrid.CheckCell(mp))
            {
                objectGrid.CreateCellObject(mp, GetTrap(currentTrap).gameObject);
            }
        }
        else
        {
            HighlightTrap(mp);
            if (Input.GetMouseButton(1))
            {
                if (objectGrid.CheckCell(mp))
                {
                    objectGrid.DeleteCellObject(mp);
                }
            }
        }
    }
        
}