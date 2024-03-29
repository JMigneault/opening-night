﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Should be a component of the Grid object. Converts to grid space.
 */
public class ToGridSpaceConverters : MonoBehaviour
{
    [SerializeField] private Grid grid;

    // convert screen space to grid coords
    public Vector2Int SSToCoords(Vector3 screenSpace)
    {
        return (Vector2Int) grid.WorldToCell(SSToWS(screenSpace));
    }

    // convert screen space to grid position (for placing objects)
    public  Vector2 SSToGPos(Vector3 screenSpace)
    {
        return ((Vector2) SSToCoords(screenSpace) + new Vector2(0.5f, 0.5f));
    }

    public Vector2 CoordsToGPos(Vector2Int coords)
    {
        return (Vector2) coords + new Vector2(.5f, .5f);
    }

    // convert screen space to world space
    public Vector3 SSToWS(Vector3 screenPos)
    {
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}
