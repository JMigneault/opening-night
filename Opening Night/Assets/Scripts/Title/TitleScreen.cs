using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{

    public Sprite[] sprites;

    private SpriteRenderer SR;
    private int spriteIndex;

    // Use this for initialization
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Invoke("UpdateFrame", 1f / 6f);
        spriteIndex = 0;
    }

    private void UpdateFrame()
    {
        spriteIndex++;
        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        SR.sprite = sprites[spriteIndex];
        Invoke("UpdateFrame", 1f / 6f);
    }
}
