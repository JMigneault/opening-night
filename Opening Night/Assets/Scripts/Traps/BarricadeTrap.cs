using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BarricadeTrap : AbstractTrap
{
    public override TrapType GetTrapType()
    {
        return TrapType.Barricade;
    }

    // no special effects
}
