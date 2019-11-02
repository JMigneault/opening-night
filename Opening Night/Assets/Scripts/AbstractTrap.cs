using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTrap : MonoBehaviour
{
    protected TrapType trapType;

    public TrapType GetTrapType()
    {
        return TrapType.StopMovement;
    }
}
