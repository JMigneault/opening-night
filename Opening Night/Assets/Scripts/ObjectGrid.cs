using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used to add and remove traps and other objects onto the grid during the setup phase.
 */
public class ObjectGrid : MonoBehaviour
{

    // for converting from world space to grid coords
    [SerializeField] private Grid grid;

    // track objects on the grid
    private Dictionary<Vector2Int, GameObject> gridObjects;


    

    // initialize gridObjects
    private void Start()
    {
        gridObjects = new Dictionary<Vector2Int, GameObject>();
    }

    /** 
     * Checks if an object exists at the given screen pos (from Input.mousePosition)
     */
    public bool CheckCell(Vector3 screenPos)
    {
        Vector2Int key = SSToCoords(screenPos);
        return gridObjects.ContainsKey(key);
    }

    /**
     * Returns the grid object at the screen pos. Should only be called if an object exists at screenPos.
     */
    public GameObject GetCellObject(Vector3 screenPos)
    {
        Vector2Int objKey = SSToCoords(screenPos);
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
    public GameObject CreateCellObject(Vector3 screenPos, GameObject prefab)
    {
        Vector2Int objKey = SSToCoords(screenPos);
        Vector2 objPos = SSToGPos(screenPos);
        if (!gridObjects.ContainsKey(objKey))
        {
            GameObject obj = Object.Instantiate(prefab, objPos, Quaternion.identity, this.transform);
            gridObjects.Add(objKey, obj);
            return obj;
        } else
        {
            Debug.Log("WARNING (CreateCellObject): Object already exists at grid coordinates: " + objKey);
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
            GameObject go = GetCellObject(screenPos);
            Vector2Int objKey = SSToCoords(screenPos);
            this.gridObjects.Remove(objKey);
            Object.Destroy(go);
        }
    }

    // convert screen space to grid coords
    private Vector2Int SSToCoords(Vector3 screenSpace)
    {
        return (Vector2Int) this.grid.WorldToCell(SSToWS(screenSpace));
    }

    // convert screen space to grid position (for placing objects)
    private Vector2 SSToGPos(Vector3 screenSpace)
    {
        return ((Vector2) SSToCoords(screenSpace) + new Vector2(0.5f, 0.5f));
    }

    // convert screen space to world space
    private Vector3 SSToWS(Vector3 screenPos)
    {
        return Camera.main.ScreenToWorldPoint(screenPos);
    }


}
