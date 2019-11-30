using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

/**
 * Used to add and remove traps and other objects onto the grid during the setup phase.
 */

[System.Serializable]
public class CoordinateRange
{
    [SerializeField] private int minX;
    [SerializeField] private int maxX;
    [SerializeField] private int minY;
    [SerializeField] private int maxY;

    public bool InRange(Vector2Int coord)
    {
        return (minX <= coord[0]) && (coord[0] <= maxX) && (minY <= coord[1]) && (coord[1] <= maxY);
    }

    public Vector2Int RandomPosition(Tilemap wallsTM)
    {
        int x = Random.Range(minX, maxX);
        int y = Random.Range(minY, maxY);
        if(wallsTM.GetTile(new Vector3Int(x, y, 0)) != null)
        {
            if(y > minY)
            {
                y--;
            }
            else
            {
                y++;
            }
        }
        return new Vector2Int(x, y);
    }
}

public class ObjectGrid : MonoBehaviour
{

    // for converting from world space to grid coords
    [SerializeField] private ToGridSpaceConverters gSpace;

    [SerializeField] CoordinateRange coordinateBounds;

    [SerializeField] Tilemap wallsTM;

    private PhotonView PV;

    // track objects on the grid
    private Dictionary<Vector2Int, AbstractCellObject> gridObjects;

    // initialize gridObjects
    private void Awake()
    {
        gridObjects = new Dictionary<Vector2Int, AbstractCellObject>();
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    /**
     * Checks if an object exists at the given screen pos (from Input.mousePosition)
     */
    public bool CheckCell(Vector3 screenPos)
    {
        return CheckCell(gSpace.SSToCoords(screenPos));
    }

    public bool CheckCell(Vector2Int coords)
    {
        return gridObjects.ContainsKey(coords);
    }

    /**
     * Returns the grid object at the screen pos. Should only be called if an object exists at screenPos.
     */
    public AbstractCellObject GetCellObject(Vector3 screenPos)
    {
        Vector2Int objKey = gSpace.SSToCoords(screenPos);
        if (gridObjects.ContainsKey(objKey))
        {
            return gridObjects[objKey];
        } else
        {
            Debug.Log("WARNING (GetCellObject): No object found at grid coordinates: " + objKey);
            return null;
        }
    }

    /**
    * Makes a copy of prefab on the grid at the screen pos. Should only be called if no object exists at screenPos.
    */
    public AbstractCellObject CreateCellObject(Vector3 screenPos, AbstractCellObject prefab)
    {
        Vector2Int objCoords = gSpace.SSToCoords(screenPos);
        //PV.RPC("CreateCellObject", RpcTarget.Others, objCoords, prefab);
        return CreateCellObject(objCoords, prefab);
    }

    [PunRPC]
    public AbstractCellObject CreateCellObject(Vector2Int objCoords, AbstractCellObject prefab)
    {
        Vector2 objPos = gSpace.CoordsToGPos(objCoords);
        if (!gridObjects.ContainsKey(objCoords))
        {
            AbstractCellObject obj = Object.Instantiate(prefab.gameObject, objPos, Quaternion.identity, this.transform).GetComponent<AbstractCellObject>();
            gridObjects.Add(objCoords, obj);
            return obj;
        }
        else
        {
            Debug.Log("WARNING (CreateCellObject): Object already exists at grid coordinates: " + objCoords);
            return null;
        }

    }

    /**
    * Delete the object on the grid at screen pos.  Should only be called if an object exists at screenPos.
    */
    public void DeleteCellObject(Vector3 screenPos)
    {
        if (CheckCell(screenPos))
        {
            Vector2Int objKey = gSpace.SSToCoords(screenPos);
            DeleteCellObject(objKey);
        }
    }

    public void DeleteCellObject(Vector2Int objKey)
    {
        if (this.gridObjects.ContainsKey(objKey))
        {
            AbstractCellObject go = this.gridObjects[objKey];
            this.gridObjects.Remove(objKey);
            go.DeleteSelf(this);
        }
    }

    public bool IsWithinBounds(Vector3 screenPos)
    {
        return coordinateBounds.InRange(gSpace.SSToCoords(screenPos)) && wallsTM.GetTile((Vector3Int) gSpace.SSToCoords(screenPos)) == null;
    }

    public Vector2Int GetCoords(Vector3 screenPos)
    {
        return gSpace.SSToCoords(screenPos);
    }

    /* NOTE: uncomment to determine the grid dimensions to set placement bounds
    private void Update()
    {
        Debug.Log(gSpace.SSToCoords(Input.mousePosition));
    } */

}
