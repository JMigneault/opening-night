using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MonsterWeapon : MonoBehaviour
{

    [SerializeField] private float freezeTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("hit player");
            collision.GetComponent<Player>().RestrictMovement(freezeTime);
        }
    }

}
