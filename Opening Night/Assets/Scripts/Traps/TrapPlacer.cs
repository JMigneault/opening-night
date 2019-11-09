﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum TrapType
{
    Barricade = 0,
    TempSlowMovement = 1,
    PermanentSlowMovement = 2,
    StopMovement = 3,
    ForceMovement = 4,
    TeleportTrap = 5,
    DoubleBarricadeVert = 6
}

// sample client code for ObjectGrid
public class TrapPlacer : MonoBehaviour
{
    [SerializeField] ObjectGrid objectGrid;
    [SerializeField] private AbstractTrap[] traps;
    [SerializeField] private TrapLimiter trapLimiter;
    private int[] trapCurrentNumber;
    private TrapType currentTrap;
    public TrapType CurrentTrap { get; }

    [SerializeField] PlacementUIManager placementUI;

    private SpriteRenderer highlightedTrapSR;
    [SerializeField] private GameObject placementSprite;
    [SerializeField] private Color highlightColor;
    [SerializeField] private PhaseManager phaseManager;
    [SerializeField] private Color validHoverColor;
    [SerializeField] private Color invalidHoverColor;

    private bool canPlace = true;
    
    //Start called once
    private void Start()
    {
        currentTrap = TrapType.Barricade;
        trapCurrentNumber = new int[Enum.GetValues(typeof(TrapType)).Length];
        for(int i = 0; i < trapCurrentNumber.Length; i++)
        {
            trapCurrentNumber[i] = 0;
        }
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
        Debug.Log(currentTrap);

    }

    public void HighlightTrap(Vector2 screenPos)
    {
        SpriteRenderer nextSR = placementUI.HighlightTrap(screenPos, highlightColor);
        if (nextSR != highlightedTrapSR)
        {
            UnhighlightTrap();
        }
        highlightedTrapSR = nextSR;
    }

    private void UnhighlightTrap()
    {
        placementUI.UnhighlightTrap(highlightedTrapSR);
    }

    private void HoverTileValid(Vector2 screenPos)
    {
        placementSprite.SetActive(true);
        Sprite sprite = GetTrap(currentTrap).GetComponent<SpriteRenderer>().sprite;
        placementSprite.GetComponent<SpriteRenderer>().sprite = sprite;
        placementSprite.GetComponent<SpriteRenderer>().color = this.validHoverColor;
        placementUI.MoveSpriteToTile(placementSprite, screenPos);
    }

    private void HoverTileInvalid(Vector2 screenPos)
    {
        placementSprite.SetActive(true);
        Sprite sprite = GetTrap(currentTrap).GetComponent<SpriteRenderer>().sprite;
        placementSprite.GetComponent<SpriteRenderer>().sprite = sprite;
        placementSprite.GetComponent<SpriteRenderer>().color = this.invalidHoverColor;
        placementUI.MoveSpriteToTile(placementSprite, screenPos);
    }

    private void UnhoverTile()
    {
        placementSprite.SetActive(false);
    }

    void CheckTrapChange()
    {
        KeyCode one = KeyCode.Alpha1;
        foreach (TrapType t in Enum.GetValues(typeof(TrapType)))
        {
            if (Input.GetKeyDown((KeyCode) ((int) one + (int) t)))
            {
                ChangeTrap(t);
            }
        }
    }

    private bool CheckTrapsRemaining(TrapType trap)
    {
        return !trapLimiter.IsLimited(trap, trapCurrentNumber[(int) trap]);
    }

    private bool CheckPlace(Vector2 mousePosition)
    {
        return Input.GetMouseButton(0) && GetTrap(currentTrap).CanPlace(objectGrid.GetCoords(mousePosition), objectGrid) && objectGrid.IsWithinBounds(mousePosition);
    }

    private bool CheckDelete(Vector2 mousePosition)
    {
        return Input.GetMouseButton(1) && objectGrid.CheckCell(mousePosition) && objectGrid.IsWithinBounds(mousePosition);
    }

    private bool CheckHighlight(Vector2 mousePosition)
    {
        return !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && objectGrid.CheckCell(mousePosition) && objectGrid.IsWithinBounds(mousePosition);
    }

    private bool CheckHover(Vector2 mousePosition)
    {
        return !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && GetTrap(currentTrap).CanPlace(objectGrid.GetCoords(mousePosition), objectGrid) && objectGrid.IsWithinBounds(mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseManager.CurrentPhase == Phase.Play) {
            UnhighlightTrap();
            UnhoverTile();
            return;
        }
        CheckTrapChange();
        Vector2 mp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        // gets input
        if (CheckPlace(mp) && CheckTrapsRemaining(currentTrap))
        {
            GetTrap(currentTrap).Place(mp, objectGrid);
            trapCurrentNumber[(int)currentTrap]++;
        }
        if (CheckDelete(mp))
        {
            objectGrid.DeleteCellObject(mp);
            trapCurrentNumber[(int) currentTrap]--;
        }
        if (CheckHighlight(mp))
        { 
            // HighlightTrap(mp);
        } else
        {
            // UnhighlightTrap();
        }
        if (CheckHover(mp) && CheckTrapsRemaining(currentTrap))
        {
            HoverTileValid(mp);
        } else
        {
            HoverTileInvalid(mp);
        }
    }
        
}