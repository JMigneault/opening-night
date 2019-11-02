using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollectKey ck = collision.gameObject.GetComponent<CollectKey>();
        if (ck != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }
}
