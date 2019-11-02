using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // change the speed of the player
    public void SetSpeed(float speed)
    {
        this.GetComponent<PlayerMovement>().SetMaxSpeed(speed);
    }

    public float GetSpeed()
    {
        return this.GetComponent<PlayerMovement>().GetMaxSpeed();
    }

    // player slides in a given direction until hitting wall/monster
    public void Slide(float speed, int direction)
    {
        //this.SetSpeed(speed);
        //will prevent player input until hits a wall
        //this.GetComponent<PlayerMovement>().SetCanMove(false);




        throw new System.NotImplementedException();
    }

}
