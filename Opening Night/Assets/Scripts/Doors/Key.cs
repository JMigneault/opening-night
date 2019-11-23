using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Key : AbstractCellObject
{

    public Vector2Int coords;
    private PlayManager playManager;
    public PlayManager PlayManager { set { this.playManager = value; } }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.tag == "Player")
        { 
            other.gameObject.GetComponent<CollectKey>().SetKey(true);
            this.gameObject.SetActive(false);
             // Note: does not remove itself from the hashmap
        }


    }
}
