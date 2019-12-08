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
            if(PlayerPrefs.GetInt("IsNavigator") == 1)
            {
                PV.RPC("RPC_GameOver", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_GameOver()
    {
        Debug.Log("Game Over");
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayManager>().SwitchToPlace();

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        gameObject.GetComponent<MonsterMovement>().ResetInputs();
        player.GetComponent<PlayerMovement>().ResetInputs();
    }
}
