using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySlowingTrap : AbstractOnEnterTrap
{
    // amount the player will decrease upon entry
    [SerializeField] private float decreasedSpeed = 1;

    private float originalSpeed;

    protected override void ActivateTrap(Player player)
    {
        //decreases player's speed temporarily
        originalSpeed = player.GetSpeed();
        player.SetSpeed(this.decreasedSpeed);
    }

    protected override void EndTrap(Player player)
    {
        //restores player's speed
        player.SetSpeed(originalSpeed);
    }

    protected override void DuringTrap(Player player)
    {
        // no effect
    }
}
