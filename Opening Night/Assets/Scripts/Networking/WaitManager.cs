using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class WaitManager : MonoBehaviourPunCallbacks
{
    private PhotonView PV;

    private GameObject StartButton;
    private GameObject Player2Text;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            StartButton = GameObject.FindGameObjectWithTag("Start");
            StartButton.SetActive(false);
            Player2Text = GameObject.FindGameObjectWithTag("Player2Text");
            Player2Text.SetActive(false);
        }
        
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }
}
