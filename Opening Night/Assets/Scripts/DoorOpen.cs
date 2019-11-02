using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollectKey player = collision.gameObject.GetComponent<CollectKey>();
        if (player != null && player.GetKey())
            Object.Destroy(this.gameObject);

    }
}
