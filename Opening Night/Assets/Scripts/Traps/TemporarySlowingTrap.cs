using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySlowingTrap : AbstractOnEnterTrap
{
    // amount the player will decrease upon entry
    [SerializeField] private float decreasedSpeed = 1;
    [SerializeField] private float decreasedDashSpeed;

    private float originalSpeed;
    private float originalDashSpeed;

    public override TrapType GetTrapType()
    {
        return TrapType.TempSlowMovement;
    }

    protected override void ActivateTrap(Player player)
    {
        // decreases player's speed temporarily
        originalSpeed = player.GetSpeed();
        player.SetSpeed(this.decreasedSpeed);
        originalDashSpeed = player.GetDashSpeed();
        player.SetDashSpeed(this.decreasedDashSpeed);
    }

    protected override void EndTrap(Player player)
    {
        //restores player's speed
        player.SetSpeed(originalSpeed);
        player.SetDashSpeed(originalDashSpeed);
    }

    protected override void DuringTrap(Player player)
    {
        // no effect
    }
}
