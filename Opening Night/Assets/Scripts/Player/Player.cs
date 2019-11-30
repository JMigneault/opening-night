using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{

    private PhotonView pv;
    private PlayerMovement movement;
    private PlayerLightRadius playerLight;
    public PlayerLightRadius PlayerLight { set { this.playerLight = value; } }

    [SerializeField] private float dashCost;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerLight != null)
        {
            if (playerLight.GetRange() > dashCost)
            {
                playerLight.SetColor(Color.white);
                if (Input.GetKeyDown(KeyCode.LeftShift) && movement.CanMove && PlayerPrefs.GetInt("IsNavigator") == 1)
                {
                    movement.Dash(playerLight, playerLight.GetRange() - this.dashCost);
                    pv.RPC("Dash", RpcTarget.Others);
                }
            }
            else
            {
                playerLight.SetColor(Color.red);
            }
        }
    }

    [PunRPC]
    private void Dash()
    {
        movement.Dash(playerLight, playerLight.GetRange() - this.dashCost);
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

    public void SetDashSpeed(float speed)
    {
        movement.SetDashSpeed(speed);
    }

    public float GetDashSpeed()
    {
        return movement.GetDashSpeed();
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
