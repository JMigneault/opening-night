using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sample client code for ObjectGrid
public class TestPlacer : MonoBehaviour
{

    [SerializeField] private GameObject gridObject;
    [SerializeField] private ObjectGrid objectGrid;

    // Update is called once per frame
    void Update()
    {
        Vector2 mp = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) {
            if (!objectGrid.CheckCell(mp))
            {
                objectGrid.CreateCellObject(mp, gridObject);
                Debug.Log(objectGrid.GetCellObject(mp));
            }
        } else if (Input.GetMouseButtonDown(1)) {
            if (objectGrid.CheckCell(mp))
            {
                Debug.Log(objectGrid.GetCellObject(mp));
                objectGrid.DeleteCellObject(mp);
            }
        }
    }
}
