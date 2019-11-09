using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLimiter : MonoBehaviour
{
    // limit number of each type of trap
    [System.Serializable]
    private class TrapLimit
    {
        [SerializeField] private TrapType trapType;
        [SerializeField] private int maxNum;

        public bool IsLimited(TrapType trapType, int numTraps)
        {
            return trapType == this.trapType && numTraps >= this.maxNum;
        }

    }

    [SerializeField] private TrapLimit[] trapLimits;

    public bool IsLimited(TrapType trapType, int numTraps)
    {
        foreach (TrapLimit tl in this.trapLimits)
        {
            if (tl.IsLimited(trapType, numTraps))
            {
                return true;
            }
        }
        return false;
    }

}
