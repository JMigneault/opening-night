using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRadius : MonoBehaviour {
    public GameObject player;
    public GameObject monster;

    private float range;
    private float intensity;
    private Light lt;
    

    void Start()
    {
        lt = GetComponent<Light>();
        range = lt.range;
        intensity = lt.intensity;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            range += Time.deltaTime * 20f;
        }
        else
        {
            range -= Time.deltaTime * 10f * ((range + 10f)/80f);
        }
        
        range = Mathf.Clamp(range, 5f, 1000f);
        lt.range = range;
    }
}
