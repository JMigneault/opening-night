using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTypeUIManager : MonoBehaviour
{
    [SerializeField] private TrapType trapType;
    [SerializeField] private TrapPlacer trapPlacer;
    [SerializeField] private UnityEngine.UIElements.TextField text;
    private int maxNum;

    private void Start()
    {
        this.maxNum = trapPlacer.TrapLimiter.GetMaxTraps(trapType);
    }

    private void OnClick()
    {
        trapPlacer.ChangeTrap(this.trapType);
    }

    private void Update()
    {
        int currentNum = 0; // todo
        text.value = currentNum + " / " + maxNum;
    }









}
