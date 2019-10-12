using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid : MonoBehaviour
{

    [SerializeField] private Grid grid;

    private Dictionary<Vector2Int, GameObject> gridObjects;

    private void Start()
    {
        gridObjects = new Dictionary<Vector2Int, GameObject>();
    }

    public bool CheckCell(Vector3 worldPos)
    {
        return false;
    }

    public GameObject GetCellObject(Vector3 screenPos)
    {
        return null;
    }

    public void AddCellObject(Vector3 screenPos, GameObject obj)
    {
        
    }

    private Vector2Int SSToCoords(Vector3 screenSpace)
    {
        return (Vector2Int) this.grid.WorldToCell(SSToWS(screenSpace));
    }


    private Vector2 SSToGPos(Vector3 screenSpace)
    {
        return ((Vector2) SSToCoords(screenSpace) + new Vector2(0.5f, 0.5f));
    }

    private Vector3 SSToWS(Vector3 screenPos)
    {
        return Camera.main.ScreenToWorldPoint(screenPos);
    }


}
