using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private GameObject Player2Text;

    [SerializeField]
    private int gameplaySceneIndex;

    void Start()
    {
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
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(gameplaySceneIndex);
    }
}
