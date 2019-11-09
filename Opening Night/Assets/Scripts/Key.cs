using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Key : AbstractCellObject
{
    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.tag == "Player")
        { 
            other.gameObject.GetComponent<CollectKey>().SetKey(true);
            Object.Destroy(this.gameObject); // Note: does not remove itself from the hashmap
        }


    }
}
