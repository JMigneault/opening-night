using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovementTrap : AbstractOnEnterTrap
{
    //how long the player will be stopped
    [SerializeField] private float stoptime = 2;



    protected override void ActivateTrap(Player player)
    {
        //player speed will be set to 0 for some stoptime seconds
        StartCoroutine(StopWait(this.stoptime, player));

    }

    protected override void DuringTrap(Player player)
    {
        //no effect
    }

    protected override void EndTrap(Player player)
    {
        //no effect
    }

    private IEnumerator StopWait(float time, Player player)
    {
        player.gameObject.GetComponent<PlayerMovement>().SetCanMove(false);
        yield return new WaitForSeconds(time);
        player.gameObject.GetComponent<PlayerMovement>().SetCanMove(true);
    }
}
