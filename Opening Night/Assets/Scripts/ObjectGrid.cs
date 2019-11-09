using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    [SerializeField] CoordinateRange keyBounds;

    [SerializeField] Tilemap wallsTM;

    [SerializeField] GameObject key;

    private Vector2Int keyPos;

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
        Vector2Int key = gSpace.SSToCoords(screenPos);
        //Checks if the current tile is where the key is
        if(key == keyPos)
        {
            return true;
        }
        Vector2Int curKey = key;
        for(int i = -1; i < 2; i++)
        {
            curKey.x = key.x + i;
            for(int j = -1; j < 2; j++)
            {
                curKey.y = key.y + j;
                if(gridObjects.ContainsKey(curKey))
                {
                    return true;
                }
            }
        }
        return gridObjects.ContainsKey(key);
    }

    /**
     * Returns the grid object at the screen pos. Should only be called if an object exists at screenPos.
     */
    public GameObject GetCellObject(Vector3 screenPos)
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
    public GameObject CreateCellObject(Vector3 screenPos, GameObject prefab)
    {
        Vector2Int objKey = gSpace.SSToCoords(screenPos);
        Vector2 objPos = gSpace.SSToGPos(screenPos);
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
            Vector2Int objKey = gSpace.SSToCoords(screenPos);
            this.gridObjects.Remove(objKey);
            Object.Destroy(go);
        }
    }

    public bool IsWithinBounds(Vector3 screenPos)
    {
        return coordinateBounds.InRange(gSpace.SSToCoords(screenPos)) && wallsTM.GetTile((Vector3Int) gSpace.SSToCoords(screenPos)) == null;
    }

    public void AddKey()
    {
        keyPos = keyBounds.RandomPosition(wallsTM);
        Instantiate(key, new Vector3(keyPos.x + .5f, keyPos.y + .5f), Quaternion.identity);
    }

    /* NOTE: uncomment to determine the grid dimensions to set placement bounds
    private void Update()
    {
        Debug.Log(gSpace.SSToCoords(Input.mousePosition));
    } */

}
