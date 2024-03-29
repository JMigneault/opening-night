﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarricadeHoriz : DoubleBarricade
{

    private Vector2Int pairOffset = new Vector2Int(1, 0);

    public override TrapType GetTrapType()
    {
        return TrapType.Barricade; // return TrapType.DoubleBarricadeHoriz; Note: if added back remake correct enum
    }

    protected override Vector2Int GetPairOffset()
    {
        return pairOffset;
    }

}
