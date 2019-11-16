using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMovement : MonoBehaviour
{

    public float maxSpeed = 10;
    public float startingSpeed = 5;
    public float accel = 0.65f;
    public float decel = 1.2f;
    public float timeFactor = 0.0001f; 
    public BoxCollider2D Collider;

    private bool canMove = true;
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
        CharAnimator = GetComponent<Animator>();
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

        this.startingSpeed += Time.deltaTime * this.timeFactor;
        this.startingSpeed = this.maxSpeed < this.startingSpeed ? this.maxSpeed : this.startingSpeed;
        if(canMove)
        {
            Vector2 input = new Vector2(0, 0);

            if(Input.GetKey(KeyCode.LeftArrow))
            {
                input.x -= 1;
                /*Vector2 scale = transform.localScale;
                scale.x = -1f * Mathf.Abs(scale.x);
                transform.localScale = scale;*/
            }

            if(Input.GetKey(KeyCode.RightArrow))
            {
                input.x += 1;
                /*Vector2 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;*/
            }

            if(Input.GetKey(KeyCode.UpArrow))
            {
                input.y += 1;
            }

            if(Input.GetKey(KeyCode.DownArrow))
            {
                input.y -= 1;
            }

            if(input.magnitude > 0)
            {
                input.Normalize();
                Vector2 dif = (input * this.startingSpeed) - Rigid.velocity;
                dif.Normalize();

                Rigid.velocity = new Vector2(
                    Approach(input.x * this.startingSpeed, Rigid.velocity.x, Mathf.Abs(dif.x) * accel),
                    Approach(input.y * this.startingSpeed, Rigid.velocity.y, Mathf.Abs(dif.y) * accel)
                );

                if(input.x > 0)
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
                }
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
                    CharAnimator.SetInteger("State", IDLE);
                }
            }

            Vector3 pos = transform.position;
            pos.z = pos.y;
            transform.position = pos;
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
                CharAnimator.SetInteger("State", IDLE);
            }
        }
    }

    public void SetCanMove(bool move)
    {
        canMove = move;
    }
}
