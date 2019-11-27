using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;

    public GameObject PlayerObject;

    private float maxSpeed;
    public float startingSpeed = 5;
    public float accel = 0.65f;
    public float decel = 1.2f;
    public BoxCollider2D Collider;

    [SerializeField] private float dashDuration;
    [SerializeField] private float dashDecay;
    [SerializeField] private float dashSpeed; 

    private bool canMove = true;
    private bool isDashing = false;
    private Rigidbody2D Rigid;
    private Animator CharAnimator;
    private Vector2 input;

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
        this.maxSpeed = this.startingSpeed; 
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
        if(PlayerPrefs.GetInt("IsNavigator") == 1)
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

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        input = new Vector2(0, 0);
        if (KeyDict[KeyCode.A])
        {
                input.x -= 1;
        }
        if(KeyDict[KeyCode.D])
        {
            input.x += 1;
        }
            if(KeyDict[KeyCode.W])
            {
                input.y += 1;
            }

            if(KeyDict[KeyCode.S])
            {
                input.y -= 1;
            }
        if (canMove || isDashing)
        {
            if (input.magnitude > 0)
            {
                input.Normalize();
                Vector2 dif = (input * maxSpeed) - Rigid.velocity;
                dif.Normalize();

                Rigid.velocity = new Vector2(
                    Approach(input.x * maxSpeed, Rigid.velocity.x, Mathf.Abs(dif.x) * accel),
                    Approach(input.y * maxSpeed, Rigid.velocity.y, Mathf.Abs(dif.y) * accel)
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

    public void Dash(PlayerLightRadius playerLight, float rangeTarget)
    {
        if (!isDashing)
        {
            CharAnimator.SetInteger("State", IDLE);
            StartCoroutine(DashCoroutine(input, playerLight, rangeTarget));
        }
    }

    private IEnumerator DashCoroutine(Vector2 direction, PlayerLightRadius playerLight, float rangeTarget)
    {
        isDashing = true;
        playerLight.ShouldDecay = false;
        float lightDecayRate = (playerLight.GetRange() - rangeTarget) / dashDuration;
        Rigid.velocity = Vector2.zero;
        float speed = dashSpeed;
        Vector2 normDirection = direction.normalized;
        float dashTime = 0.0f;
        while (dashTime < dashDuration)
        {
            CharAnimator.SetInteger("State", IDLE);
            Rigid.velocity = speed * direction;
            speed = Mathf.Abs(dashSpeed - (dashDecay * dashTime * dashTime)); // quadratic decay
            dashTime += Time.deltaTime;
            playerLight.AdjustRange(-1 * Time.deltaTime * lightDecayRate);
            yield return new WaitForFixedUpdate();
        }
        Rigid.velocity = Vector2.zero;
        playerLight.ShouldDecay = true;
        isDashing = false;
    }

    public void SetCanMove(bool move)
    {
        canMove = move;
    }

    public void SetMaxSpeed(float newSpeed)
    {
        this.maxSpeed = newSpeed;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
}
