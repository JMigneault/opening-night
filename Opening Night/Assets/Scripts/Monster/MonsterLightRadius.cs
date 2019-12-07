using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLightRadius : MonoBehaviour {
    public GameObject Player;
    public GameObject Monster;
    public Camera Cam;

    private float range;
    private float intensity;
    private Light lt;
    

    void Start()
    {
        lt = GetComponent<Light>();
        //range = lt.range;
        //intensity = lt.intensity;
    }

    void Update()
    {
        if(Monster == null)
        {
            Monster = GameObject.FindGameObjectWithTag("Monster");
        }
        else
        {
            transform.position = new Vector3(Monster.transform.position.x, Monster.transform.position.y + 1f, transform.position.z);
            //lt.range = range * (8f / Cam.orthographicSize);
        }
        
    }
}
