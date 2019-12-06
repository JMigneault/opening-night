using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls scaling and vertical placement of backgrounds to work on all aspect ratios.
/// </summary>
public class BackgroundScaler : MonoBehaviour
{
    private float scaleX = 1f;
    private float scaleY = 1f;

    void Awake()
    {
        SpriteRenderer SR = GetComponent<SpriteRenderer>();

        float width = SR.sprite.bounds.size.x;
        float height = SR.sprite.bounds.size.y;
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = (screenHeight * Screen.width) / Screen.height;

        //Sets the scale to fit the larger of the width and height
        transform.localScale = new Vector3(screenWidth / width, screenHeight / height, 1f);
    }
}
