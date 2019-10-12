using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public float speed = 500;

    private float horz;
    private float vert;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Time.deltaTime * speed * horz, Time.deltaTime * speed * vert);
    }
}
