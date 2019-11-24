using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTrap : AbstractCellObject
{
    protected TrapType trapType;

    public abstract TrapType GetTrapType();

    public virtual void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    virtual public void Rotate()
    {
        // no effect
    }

}
