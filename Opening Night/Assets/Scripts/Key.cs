using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    { 
        CollectKey keyComp = other.gameObject.GetComponent<CollectKey>();
        if (keyComp != null)
        { 
            keyComp.SetKey(true);
            Destroy(this.gameObject);
        }


    }

}
