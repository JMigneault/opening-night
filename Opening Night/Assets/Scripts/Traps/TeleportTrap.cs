using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrap : AbstractOnEnterTrap
{
    // amount the player will decrease upon entry
    [SerializeField] private Vector2 destination;

    public override TrapType GetTrapType()
    {
        return TrapType.TeleportTrap;
    }

    protected override void ActivateTrap(Player player)
    {
        // teleports player to receiver
        player.gameObject.transform.position = destination;
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
