using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovementTrap : AbstractOnEnterTrap
{
    //how long the player will be stopped
    [SerializeField] private float stoptime;
   
    protected override void ActivateTrap(Player player)
    {
        //player speed will be set to 0 for some set time
        throw new System.NotImplementedException();
    }

    protected override void DuringTrap(Player player)
    {
        //no effect
    }

    protected override void EndTrap(Player player)
    {
        //no effect
    }
}
