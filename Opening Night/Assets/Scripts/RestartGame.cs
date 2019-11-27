using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RestartGame : MonoBehaviour
{

    private PhotonView PV;

    private GameObject player;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if(collision.gameObject.tag == "Player")
        {
            if(player == null)
            {
                player = collision.gameObject;
            }
            PV.RPC("RPC_GameOver", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_GameOver()
    {
        Debug.Log("restart game");
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayManager>().SwitchToPlace();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        gameObject.GetComponent<MonsterMovement>().ResetInputs();
        player.GetComponent<PlayerMovement>().ResetInputs();
    }
}
