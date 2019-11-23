using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            //myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), spawn.position, spawn.rotation, 0);
        }
    }
}
