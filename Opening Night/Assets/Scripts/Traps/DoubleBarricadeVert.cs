using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarricadeVert : DoubleBarricade
{
    private Vector2Int pairOffset = new Vector2Int(0, 1);

    public override TrapType GetTrapType()
    {
        return TrapType.DoubleBarricadeVert;
    }

    protected override Vector2Int GetPairOffset()
    {
        return pairOffset;
    }
}
