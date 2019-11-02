﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTrap : AbstractOnEnterTrap
{
    // amount the player will decrease upon entry
    [SerializeField] private float decreasedSpeed = 3;

    protected override void ActivateTrap(Player player)
    {
        //decreases player's speed permanently
        player.SetSpeed(this.decreasedSpeed);
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