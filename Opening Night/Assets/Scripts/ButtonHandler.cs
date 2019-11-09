using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonHandler : MonoBehaviour
{
    TrapPlacer tp = new TrapPlacer();

   public void SetColor(GameObject trap)
    {
        
     

}
    public void ChangeTrap(TrapType trapType)
    {
        tp.ChangeTrap(TrapType.SlowMovement);
    }
}
