using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMovementTrap : AbstractOnEnterTrap
{
    [SerializeField] private float time;

    protected override void ActivateTrap(Player player)
    {
        StartCoroutine(ForceMovement(this.time, player));
    }

    protected override void EndTrap(Player player)
    {
        // no effect
    }

    protected override void DuringTrap(Player player)
    {
        // no effect
    }

    private IEnumerator ForceMovement(float time, Player player)
    {
        player.LockMovement();
        yield return new WaitForSeconds(time);
        player.UnlockMovement();
    }
}
