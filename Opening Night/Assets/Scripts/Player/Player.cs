using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerLightRadius playerLight;
    public PlayerLightRadius PlayerLight { set { this.playerLight = value; } }

    [SerializeField] private float dashCost;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerLight != null)
        {
            if (playerLight.GetRange() > dashCost)
            {
                playerLight.SetColor(Color.white);
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    movement.Dash(playerLight, playerLight.GetRange() - dashCost);
                    playerLight.AdjustRange(-1 * dashCost);
                }
            }
            else
            {
                playerLight.SetColor(Color.red);
            }
        }
    }

    // change the speed of the player
    public void SetSpeed(float speed)
    {
        movement.SetMaxSpeed(speed);
    }

    public float GetSpeed()
    {
        return movement.GetMaxSpeed();
    }

    public void RestrictMovement(float time)
    {
        movement.SetCanMove(false);
        Invoke("UnrestrictMovement", time);
    }

    private void UnrestrictMovement()
    {
        movement.SetCanMove(true);
    }



}
