using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLimiter : MonoBehaviour
{
    // limit number of each type of trap
    [System.Serializable]
    private class TrapLimit
    {
        [SerializeField] private TrapType trap;
        public TrapType Trap { get { return trap; } }
        [SerializeField] private int maxNum;
        public int MaxNum { get { return maxNum; } }

        public bool IsLimited(TrapType trap, int numTraps)
        {
            return trap == this.trap && numTraps >= this.maxNum;
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

    public int GetMaxTraps(TrapType trapType)
    {
        foreach (TrapLimit tl in this.trapLimits)
        {
            if (tl.Trap == trapType) { return tl.MaxNum; }
        }
        Debug.LogError("GetMaxTraps: Did not find trap type: " + trapType);
        return -1;
    }
}
