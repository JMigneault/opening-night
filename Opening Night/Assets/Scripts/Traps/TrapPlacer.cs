using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

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
    [SerializeField] private AbstractTrap[] trapPrefabs;
    [SerializeField] private TrapLimiter trapLimiter;
    public TrapLimiter TrapLimiter { get { return trapLimiter; } }
    private int[] trapCurrentNumber;
    private TrapType currentTrap;
    public TrapType CurrentTrap { get { return currentTrap; } }
    private AbstractTrap[] traps;

    [SerializeField] PlacementUIManager placementUI;

    private AbstractTrap highlightedTrap;
    [SerializeField] private GameObject placementSprite;
    [SerializeField] private Color highlightColor;
    [SerializeField] private PhaseManager phaseManager;
    [SerializeField] private Color validHoverColor;
    [SerializeField] private Color invalidHoverColor;

    private bool canPlace = true;

    private PhotonView PV;
    //Start called once
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        traps = new AbstractTrap[trapPrefabs.Length];
        for (int i = 0; i < trapPrefabs.Length; i++)
        {
            traps[i] = Instantiate<AbstractTrap>(trapPrefabs[i], new Vector2(-100, 0), Quaternion.identity);
        }
        currentTrap = TrapType.Barricade;
        trapCurrentNumber = new int[Enum.GetValues(typeof(TrapType)).Length];
        for(int i = 0; i < trapCurrentNumber.Length; i++)
        {
            trapCurrentNumber[i] = 0;
        }
        highlightedTrap = null;
    }

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

    [PunRPC]
    public void ChangeTrap(TrapType trapType)
    {
        this.currentTrap = trapType;
        if (PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            PV.RPC("ChangeTrap", RpcTarget.Others, trapType);
        }
    }

    private void IncrementTrap()
    {
        if (trapLimiter.IsLimited(currentTrap, trapCurrentNumber[(int)currentTrap]))
        {
            for (int i = 0; i < Enum.GetValues(typeof(TrapType)).Length; i++)
            {
                if (!trapLimiter.IsLimited((TrapType) i, trapCurrentNumber[i])) {
                    currentTrap = (TrapType)i;
                    return;
                }
            }
        }
    }

    public void HighlightTrap(Vector2 screenPos)
    {
        AbstractTrap nextTrap = placementUI.HighlightTrap(screenPos, highlightColor);
        if (nextTrap != highlightedTrap)
        {
            UnhighlightTrap();
        }
        highlightedTrap = nextTrap;
    }

    private void UnhighlightTrap()
    {
        placementUI.UnhighlightTrap(highlightedTrap);
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

    void CheckForRotate()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            RotateCurrentTrap();
        }
    }

    [PunRPC]
    private void RotateCurrentTrap()
    {
        GetTrap(currentTrap).Rotate();
        if (PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            PV.RPC("RotateCurrentTrap", RpcTarget.Others);
        }
    }

    private bool CheckTrapsRemaining(TrapType trap)
    {
        return !trapLimiter.IsLimited(trap, trapCurrentNumber[(int)trap]);
    }

    public int GetNumRemaining(TrapType trap)
    {
        return trapCurrentNumber[(int)trap];
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

    private bool CheckHoverValid(Vector2 mousePosition)
    {
        return !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && GetTrap(currentTrap).CanPlace(objectGrid.GetCoords(mousePosition), objectGrid) && CheckHoverBounds(mousePosition);
    }

    private bool CheckHoverBounds(Vector2 mousePosition)
    {
        return objectGrid.IsWithinBounds(mousePosition);
    }

    [PunRPC]
    private void PlaceTrap(Vector2 coords)
    {
        GetTrap(currentTrap).Place(new Vector2Int((int)coords.x, (int)coords.y), objectGrid);
        if (PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            PV.RPC("PlaceTrap", RpcTarget.Others, coords);
        }
    }


    [PunRPC]
    private void DeleteTrap(Vector2 coords)
    {
        objectGrid.DeleteCellObject(new Vector2Int((int)coords.x, (int)coords.y));
        if (PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            PV.RPC("DeleteTrap", RpcTarget.Others, coords);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseManager.CurrentPhase == Phase.Play || PlayerPrefs.GetInt("IsNavigator") == 1) {
            UnhighlightTrap();
            UnhoverTile();
            IncrementTrap();
            return;
        }
        CheckTrapChange();
        CheckForRotate();
        Vector2 mp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (CheckPlace(mp) && CheckTrapsRemaining(currentTrap))
        {
            Debug.Log("trap placed " + mp);
            Vector2Int gridPos = objectGrid.GetCoords(mp);
            if(PV)
            {
                PV.RPC("RPC_PlaceTrap", RpcTarget.Others, (byte)currentTrap, gridPos.x, gridPos.y);

            }
            GetTrap(currentTrap).Place(gridPos, objectGrid);
            trapCurrentNumber[(int)currentTrap]++;
            PlaceTrap(objectGrid.GetCoords(mp));
        }
        if (CheckDelete(mp))
        {
            trapCurrentNumber[(int)((AbstractTrap)objectGrid.GetCellObject(mp)).GetTrapType()]--;
            DeleteTrap(objectGrid.GetCoords(mp));
        }
        if (CheckHighlight(mp))
        {
            HighlightTrap(mp);
        } else
        {
            UnhighlightTrap();
        }
        if (CheckHoverValid(mp) && CheckTrapsRemaining(currentTrap))
        {
            HoverTileValid(mp);
        } else
        {
            HoverTileInvalid(mp);
        }
        if (!CheckHoverBounds(mp))
        {
            UnhoverTile();
        }
        IncrementTrap(); // moves to next trap if you have run out
    }

    [PunRPC]
    void RPC_PlaceTrap(Byte currentTrap, int gridPosX, int gridPosY)
    {
        Debug.Log("received trap");
        GetTrap((TrapType)currentTrap).Place(new Vector2Int(gridPosX, gridPosY), objectGrid);
    }

}
