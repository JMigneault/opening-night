using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleCameraController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Monster;
    public Camera Cam;

    public Vector2 Offset;
    public float smoothTime = .5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector2 velocity;

    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        Vector2 centerPoint = GetCenterPoint();
        Vector2 newPosition = centerPoint + Offset;
        Vector2 xy = Vector2.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        transform.position = new Vector3(xy.x, xy.y, -14f);
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private float GetGreatestDistance()
    {
        Bounds bounds = new Bounds(Player.transform.position, Vector2.zero);
        bounds.Encapsulate(Monster.transform.position);
        return bounds.size.x;
    }

    private Vector2 GetCenterPoint()
    {
        Bounds bounds = new Bounds(Player.transform.position, Vector2.zero);
        bounds.Encapsulate(Monster.transform.position);
        return bounds.center;
    }
}