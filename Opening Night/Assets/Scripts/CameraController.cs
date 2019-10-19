using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject Player;
    public GameObject Monster;
    public float yOffset;
    public Camera Cam;
    public Light PointLight;

    private Vector3 offset;

    private float shake;
    private Vector3 shakeOffset;

    void Start()
    {
        offset = transform.position - Player.transform.position;
        offset.y += yOffset;
        shake = 0;
        shakeOffset = new Vector3(0, 0);
    }

    public void ShakeOffset(Vector3 direction, float amount)
    {
        shakeOffset += direction * amount;
    }

    public void Shake(float amount)
    {
        shake = amount;
    }

    private float Approach(float target, float starting, float delta)
    {
        float dif = target - starting;
        if(dif > delta)
        {
            return starting + delta;
        }
        else if(dif < -delta)
        {
            return starting - delta;
        }
        else
        {
            return target;
        }
    }

    void FixedUpdate()
    {
        if(Player != null)
        {
            Vector3 target = ((Player.transform.position + Monster.transform.position) / 2f) + offset;
            if(shake >= 0)
            {
                float angle = Random.value * (Mathf.PI * 2);
                target += new Vector3(Mathf.Cos(angle) * shake / 16.0f, Mathf.Sin(angle) * shake / 16.0f);
                shake -= Time.deltaTime * 3;
            }

            target += shakeOffset;
            shakeOffset = new Vector3(
                Approach(0, shakeOffset.x, Mathf.Abs(shakeOffset.normalized.x) * Time.unscaledDeltaTime * 50f),
                Approach(0, shakeOffset.y, Mathf.Abs(shakeOffset.normalized.y) * Time.unscaledDeltaTime * 50f)
            );

            transform.position = Vector3.Lerp(transform.position, target, Time.unscaledDeltaTime * 8f);

            Cam.orthographicSize = Vector2.Distance(Player.transform.position, Monster.transform.position) / 2f;
        }
       
    }
}