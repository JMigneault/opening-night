using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLightController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("IsNavigator") == 1)
        {
            this.gameObject.SetActive(false);
        }
    }
}
