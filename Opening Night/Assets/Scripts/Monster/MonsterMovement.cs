using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MonsterMovement : MonoBehaviour
{
    private PhotonView PV;

    public float maxSpeed = 10;
    public float startingSpeed = 2;
    public float accel = 0.65f;
    public float decel = 1.2f;
    public float timeFactor = 0.1f; 
    public BoxCollider2D Collider;

    private bool canMove = true;
    private Rigidbody2D Rigid;
    private Animator CharAnimator;
    private float speed;
    

    private const int WALK_RIGHT = 0;
    private const int WALK_FORWARD = 1;
    private const int WALK_LEFT = 2;
    private const int WALK_BACKWARD = 3;
    private const int IDLE = 4;

    private Dictionary<KeyCode, bool> KeyDict;

    // Use this for initialization
    void Start()
    {
        PV = GetComponent<PhotonView>();

        Rigid = GetComponent<Rigidbody2D>();
        CharAnimator = GetComponent<Animator>();
        this.resetSpeed();

        KeyDict = new Dictionary<KeyCode, bool>();
        KeyDict[KeyCode.W] = false;
        KeyDict[KeyCode.S] = false;
        KeyDict[KeyCode.A] = false;
        KeyDict[KeyCode.D] = false;
    }

    public void resetSpeed()
    {
        this.speed = this.startingSpeed;
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

    private void Update()
    {
        if(PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            UpdateControls();
        }
    }

    private void UpdateControls()
    {
        bool changed = false;

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.W))
        {
            KeyDict[KeyCode.W] = Input.GetKey(KeyCode.W);
            changed = true;
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.S))
        {
            KeyDict[KeyCode.S] = Input.GetKey(KeyCode.S);
            changed = true;
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.A))
        {
            KeyDict[KeyCode.A] = Input.GetKey(KeyCode.A);
            changed = true;
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.D))
        {
            KeyDict[KeyCode.D] = Input.GetKey(KeyCode.D);
            changed = true;
        }

        if(changed)
        {
            PV.RPC("RPC_UpdateInput", RpcTarget.Others, KeyDict[KeyCode.W], KeyDict[KeyCode.S], KeyDict[KeyCode.A], KeyDict[KeyCode.D], transform.position);
        }
    }

    [PunRPC]
    void RPC_UpdateInput(bool W, bool S, bool A, bool D, Vector3 pos)
    {
        KeyDict[KeyCode.W] = W;
        KeyDict[KeyCode.S] = S;
        KeyDict[KeyCode.A] = A;
        KeyDict[KeyCode.D] = D;
        transform.position = Vector3.MoveTowards(transform.position, pos, maxSpeed);
    }

    void FixedUpdate()
    {
        Movement(); 
    }

    private void Movement()
    {
        this.speed += Time.deltaTime * this.timeFactor;
        this.speed = this.maxSpeed < this.speed ? this.maxSpeed : this.speed;
        if(canMove)
        {
            Vector2 input = new Vector2(0, 0);

            if(KeyDict[KeyCode.A])
            {
                input.x -= 1;
                /*Vector2 scale = transform.localScale;
                scale.x = -1f * Mathf.Abs(scale.x);
                transform.localScale = scale;*/
            }

            if(KeyDict[KeyCode.D])
            {
                input.x += 1;
                /*Vector2 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;*/
            }

            if(KeyDict[KeyCode.W])
            {
                input.y += 1;
            }

            if(KeyDict[KeyCode.S])
            {
                input.y -= 1;
            }

            if(input.magnitude > 0)
            {
                input.Normalize();
                Vector2 dif = (input * this.speed) - Rigid.velocity;
                dif.Normalize();

                Rigid.velocity = new Vector2(
                    Approach(input.x * this.speed, Rigid.velocity.x, Mathf.Abs(dif.x) * accel),
                    Approach(input.y * this.speed, Rigid.velocity.y, Mathf.Abs(dif.y) * accel)
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
