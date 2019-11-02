using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTrap : MonoBehaviour
{
    private TrapType trapType;

    public TrapType GetTrapType()
    {
        return this.trapType;
    }
}
