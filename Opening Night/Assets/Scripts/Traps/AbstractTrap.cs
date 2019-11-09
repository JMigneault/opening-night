using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTrap : AbstractCellObject
{
    protected TrapType trapType;

    public abstract TrapType GetTrapType();

}
