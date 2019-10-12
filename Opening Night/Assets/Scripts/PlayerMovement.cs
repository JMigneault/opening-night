using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 5;
    public float accel = 0.65f;
    public float decel = 1.2f;
    public BoxCollider2D Collider;

    private Rigidbody2D Rigid;
    private Animator CharAnimator;

    private const int WALK_RIGHT = 0;
    private const int WALK_FORWARD = 1;
    private const int WALK_LEFT = 2;
    private const int WALK_BACKWARD = 3;
    private const int IDLE = 4;

    // Use this for initialization
    void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();
        //CharAnimator = GetComponent<Animator>();
    }

    private float Approach(float target, float starting, float delta)
    {
        float dif = target - starting;
        if(dif > delta)
        {
            return starting + delta;
        }
        else if(dif < -delta)
        {
            return starting - delta;
        }
        else
        {
            return target;
        }
    }

    void Update()
    {
            Vector2 input = new Vector2(0, 0);

            if(Input.GetKey(KeyCode.A))
            {
                input.x -= 1;
            }

            if(Input.GetKey(KeyCode.D))
            {
                input.x += 1;
            }

            if(Input.GetKey(KeyCode.W))
            {
                input.y += 1;
            }

            if(Input.GetKey(KeyCode.S))
            {
                input.y -= 1;
            }

            if(input.magnitude > 0)
            {
                input.Normalize();
                Vector2 dif = (input * maxSpeed) - Rigid.velocity;
                dif.Normalize();

                Rigid.velocity = new Vector2(
                    Approach(input.x * maxSpeed, Rigid.velocity.x, Mathf.Abs(dif.x) * accel),
                    Approach(input.y * maxSpeed, Rigid.velocity.y, Mathf.Abs(dif.y) * accel)
                );

                /*if(input.x > 0)
                {
                    CharAnimator.SetInteger("State", WALK_RIGHT);
                }
                else if(input.x < 0)
                {
                    CharAnimator.SetInteger("State", WALK_LEFT);
                }
                else
                {
                    if(input.y > 0)
                    {
                        CharAnimator.SetInteger("State", WALK_BACKWARD);
                    }
                    else
                    {
                        CharAnimator.SetInteger("State", WALK_FORWARD);
                    }
                }*/
            }
            else
            {
                Vector2 vel = Rigid.velocity.normalized;
                Rigid.velocity = new Vector2(
                    Approach(0, Rigid.velocity.x, Mathf.Abs(vel.x) * decel),
                    Approach(0, Rigid.velocity.y, Mathf.Abs(vel.y) * decel)
                );
                if(Rigid.velocity.x == 0 && Rigid.velocity.y == 0)
                {
                    //CharAnimator.SetInteger("State", IDLE);
                }
            }
    }
}
