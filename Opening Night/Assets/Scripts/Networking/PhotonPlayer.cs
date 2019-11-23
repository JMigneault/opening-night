using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myGameInfo;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            myGameInfo = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonGameInfo"), transform.position, transform.rotation, 0);
        }
    }
}
