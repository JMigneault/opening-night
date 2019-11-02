using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMovementTrap : AbstractOnEnterTrap
{
    [SerializeField] private float slideSpeed = 5;

    // 0 is right, 1 is forward, 2 is left, 3 is back
    [SerializeField] private int direction = 1;

    protected override void ActivateTrap(Player player)
    {
        //forces player to slide in a direction
        player.Slide(slideSpeed, direction);
    }

    protected override void EndTrap(Player player)
    {
        // no effect
    }

    protected override void DuringTrap(Player player)
    {
        // no effect
    }
}
