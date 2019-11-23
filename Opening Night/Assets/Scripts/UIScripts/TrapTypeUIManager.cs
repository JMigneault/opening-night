using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapTypeUIManager : MonoBehaviour
{
    [SerializeField] private TrapType trapType;
    [SerializeField] private TrapPlacer trapPlacer;
    // [SerializeField] private UnityEngine.UI.Text text;
    private int maxNum;

    private void Start()
    {
        this.maxNum = trapPlacer.TrapLimiter.GetMaxTraps(trapType);
        // this.test = GetComponentInChildren<Text>();
    }

    public void ChangeTrap()
    {
        trapPlacer.ChangeTrap(this.trapType);
    }

    private void Update()
    {
        // text.value = trapPlacer.GetNumRemaining(trapType) + " / " + maxNum;
    }









}
