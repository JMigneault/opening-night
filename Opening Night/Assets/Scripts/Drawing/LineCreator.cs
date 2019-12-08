using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles receiving touch input and sending those inputs to the currently active line.
/// This script attached to LineManager in each level.
/// </summary>
public class LineCreator : MonoBehaviour
{
    //Reference of the Line Prefab used to display the line.
    public GameObject LinePrefab;

    private Line activeLine;

    /// <summary>
    /// Handle drawing by keeping track of active lines.
    /// An active line is one that is currently being drawn.
    /// An active line becomes inactive when it is no longer being drawn.
    /// </summary>
    private void Update()
    {
        if(PlayerPrefs.GetInt("IsNavigator") == 1)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject lineObj = Instantiate(LinePrefab);
                activeLine = lineObj.GetComponent<Line>();
            }
            else if(Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = -90f;
                activeLine.UpdateLine(mousePos);
            }
        }
        
    }
}
