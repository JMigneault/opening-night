using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
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

    // player slides in a given direction until hitting wall/monster
    public void Slide(float speed, int direction)
    {
        //this.SetSpeed(speed);
        //will prevent player input until hits a wall
        //this.GetComponent<PlayerMovement>().SetCanMove(false);




        throw new System.NotImplementedException();
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
