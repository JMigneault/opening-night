using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{

    [SerializeField] private PlayManager playManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<CollectKey>().GetKey())
        {
            CollectKey keyScript = collision.gameObject.GetComponent<CollectKey>();
            if(keyScript.GetKey())
            {
                keyScript.SetKey(false);
                playManager.SwitchToPlace();
            }
        }
    }
}
