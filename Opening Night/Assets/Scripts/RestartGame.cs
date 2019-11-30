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
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayManager>().SwitchToPlace();

        gameObject.GetComponent<MonsterMovement>().ResetInputs();
        player.GetComponent<PlayerMovement>().ResetInputs();
    }
}
