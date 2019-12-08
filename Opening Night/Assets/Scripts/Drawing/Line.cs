using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles updating a line's line renderer and edge collider with given input.
/// This script attached to the Line prefab.
/// </summary>
public class Line : MonoBehaviour
{
    [Tooltip("The minimum distance needed to register a new point in the line")]
    public float MinimumDrawDistance;
    [Tooltip("The maximum distance a two points of a line can have.")]
    public float MaximumDrawDistance;

    //Reference to line renderer of the line
    private LineRenderer lineRenderer;

    //List of all points in the line
    private List<Vector3> points;

    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// If the line does not have existing points, instantiate a new list of Vector2 with the new point.
    /// If the line has existing points, only add new point if it meets the minimum distance from the last point in the list.
    /// </summary>
    /// <param name="touchPoint">Position of touch input </param>
    public void UpdateLine(Vector3 touchPoint)
    {
        if(points == null)
        {
            points = new List<Vector3>();
            SetPoint(touchPoint);
            return;
        }

        float dis = Vector2.Distance(points.Last(), touchPoint);
        if(dis > MinimumDrawDistance && dis < MaximumDrawDistance)
        {
            SetPoint(touchPoint);
        }
    }

    /// <summary>
    /// Adds point to the points List. Updates line renderer. Updates edge collider if there are at least two points in the list.
    /// If there are not two points in the list, the edge collider will break otherwise.
    /// </summary>
    /// <param name="point"></param>
    private void SetPoint(Vector3 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }
}
