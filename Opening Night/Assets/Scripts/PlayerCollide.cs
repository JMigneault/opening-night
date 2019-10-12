using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
            Rigidbody2D playerRB = collision.collider.attachedRigidbody;
            playerRB.velocity = new Vector2(0, 0);
    }
}
