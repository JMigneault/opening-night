using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTrapScript : MonoBehaviour
{
    private Collider collider;

    //amount the player will decrease upon entry
    public float speedDecrease;

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
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<PlayerMovement>().ChangeSpeed();
            Debug.Log("Slow down player movement");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Exit");
        }
    }

}
