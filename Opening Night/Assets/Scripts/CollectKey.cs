using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{


    private bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
       this.hasKey = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKey(bool shouldHaveKey) {
        this.hasKey = shouldHaveKey;
    }

    public bool GetKey() {

        return this.hasKey;
    }

}
