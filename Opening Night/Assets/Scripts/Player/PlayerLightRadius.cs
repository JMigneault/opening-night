using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightRadius : MonoBehaviour {
    public GameObject Player;
    public GameObject Monster;
    public Camera Cam;
    public Camera Cam2;

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
            Player.GetComponent<PlayerMovement>().SetCanMove(false);
            range += Time.deltaTime * 20f;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            Player.GetComponent<PlayerMovement>().SetCanMove(true);
        }
        else
        {
            range -= Time.deltaTime * 20f * ((range + 10f)/80f);
        }
        
        range = Mathf.Clamp(range, 3f, 20f);

        Vector2 trans = Player.transform.position;
        Vector2 worldVec = trans;
        transform.position = new Vector3(worldVec.x, worldVec.y + .6f, transform.position.z);
        lt.range = range;// * (8f / Cam.orthographicSize);
    }
}
