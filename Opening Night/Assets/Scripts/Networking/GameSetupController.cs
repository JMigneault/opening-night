using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    private PhotonView PV;

    [SerializeField]
    private int gameplaySceneIndex;
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private GameObject Player2Text;
    [SerializeField]
    private GameObject Player2Image;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        if(!StartButton.activeInHierarchy && GameObject.FindGameObjectsWithTag("PhotonPlayer").Length > 1)
        {
            StartButton.SetActive(true);
            Player2Text.SetActive(true);
            Player2Image.SetActive(true);
        }
    }

    public void StartGame()
    {
        int isNavigator = Random.Range(0, 1);
        PlayerInfo.PI.IsNavigator = isNavigator;
        PlayerPrefs.SetInt("IsNavigator", isNavigator);
        Debug.Log("NAVIGATOR: " + PlayerPrefs.GetInt("IsNavigator"));
        PV.RPC("RPC_IsNavigator", RpcTarget.Others, isNavigator);

        PV.RPC("RPC_StartGame", RpcTarget.All);
    }

    [PunRPC]
    void RPC_StartGame()
    {
        Debug.Log("start");
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(gameplaySceneIndex);
        }
    }

    [PunRPC]
    void RPC_IsNavigator(int navigator)
    {
        int isNavigator = 1 - navigator;
        PlayerInfo.PI.IsNavigator = isNavigator;
        PlayerPrefs.SetInt("IsNavigator", isNavigator);
        Debug.Log("NAVIGATOR: " + PlayerPrefs.GetInt("IsNavigator"));
    }
}
