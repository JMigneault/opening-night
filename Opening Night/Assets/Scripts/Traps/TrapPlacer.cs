using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    Barrier = 0,
    SlowMovement = 1,
    StopMovement = 2,
    ForceMovement = 3,
    PermanentSlow = 4,
    TeleporterReceiver = 5,


}


// sample client code for ObjectGrid
public class TrapPlacer : MonoBehaviour
{
    [SerializeField] ObjectGrid objectGrid;
    public GameObject[] traps;
    public static TrapType currentTrap;

    [SerializeField] PlacementUIManager placementUI;

    private SpriteRenderer highlightedTrapSR;
    [SerializeField] private GameObject placementSprite;
    [SerializeField] private Color highlightColor;
    [SerializeField] private float highlightAlpha;


    //Start called once
    private void Start()
    {
        currentTrap = TrapType.StopMovement;
        highlightedTrapSR = null;
    }

    //method for setting current trap
    private GameObject GetTrap(TrapType trapType)
    {
        foreach (GameObject trap in traps)
        {
            if (currentTrap == trapType)
            {
                return trap;
            }
        }
        Debug.LogError("Get Trap: No matching trap found.");
        return null;
    }

    public void ChangeTrap(TrapType trapType)
    {
        currentTrap = trapType;
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
            placementSprite.SetActive(true);
            Sprite sprite = GetTrap(currentTrap).GetComponent<Sprite>();
            placementSprite.GetComponent<SpriteRenderer>().sprite = sprite;
            placementUI.MoveSpriteToTile(placementSprite, screenPos);
        }
        else
        {
            placementSprite.SetActive(false);
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