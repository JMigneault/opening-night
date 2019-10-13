using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sample client code for ObjectGrid
public class TestPlacer : MonoBehaviour
{

    [SerializeField] GameObject gridObject1;
    [SerializeField] GameObject gridObject2;
    [SerializeField] GameObject gridObject3;
    [SerializeField] ObjectGrid objectGrid;
    public GameObject[] traps;
    [SerializeField] GameObject currentTrap;

    //Start called once
    private void Start()
    {
        currentTrap = traps[1];
    }

    // Update is called once per frame
    void Update()
    {

        //Selecting type of trap
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeTrap(1);
        }else if (Input.GetKeyDown(KeyCode.Alpha2)){

            changeTrap(2);
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeTrap(3);
        }

        //method for setting current trap
        void changeTrap(int num)
        {


            for(int i = 0; i < traps.Length; i++)
            {
                if(i == num)
                {
                    currentTrap = traps[i];
                }

            }
        }

        Vector2 mp = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) {
            if (!objectGrid.CheckCell(mp))
            {
                objectGrid.CreateCellObject(mp, currentTrap);
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
