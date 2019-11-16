using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    [SerializeField] PlayManager playManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if(collision.gameObject.tag == "Player")
        {
            playManager.SwitchToPlace();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
