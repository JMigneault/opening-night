using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public static PlayerInfo PI;

    public int IsNavigator;

    private void OnEnable()
    {
        if(PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if(PlayerInfo.PI != this)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("IsNavigator"))
        {
            IsNavigator = PlayerPrefs.GetInt("IsNaviagator");
        }
        else
        {
            IsNavigator = 0;
            PlayerPrefs.SetInt("IsNavigator", IsNavigator);
        }
    }
}
