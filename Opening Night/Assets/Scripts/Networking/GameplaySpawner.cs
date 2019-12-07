using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameplaySpawner : MonoBehaviourPunCallbacks
{

    private PhotonView PV;

    public Transform playerSpawn;
    public Transform monsterSpawn;

    [SerializeField]
    private CameraController MainCamera;

    [SerializeField] 
    private PlayManager Manager;

    private GameObject monsterObj;
    private GameObject navigatorObj;

    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if(PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            monsterObj = PhotonNetwork.Instantiate(Path.Combine("GamePrefabs", "Monster"), monsterSpawn.position, monsterSpawn.rotation, 0);
            Manager.SetMonster(monsterObj);
            PV.RPC("RPC_MonsterInit", RpcTarget.Others);
            MainCamera.SetTarget(monsterObj);
            MainCamera.SetMonsterTarget();
        }
        else
        {
            navigatorObj = PhotonNetwork.Instantiate(Path.Combine("GamePrefabs", "Player"), playerSpawn.position, playerSpawn.rotation, 0);
            Manager.SetNavigator(navigatorObj);
            PV.RPC("RPC_NavigatorInit", RpcTarget.Others);
            MainCamera.SetTarget(navigatorObj);
            MainCamera.SetPlayerTarget();
        }
    }

    [PunRPC]
    void RPC_MonsterInit()
    {
        Debug.Log("monster please");
        monsterObj = GameObject.FindGameObjectWithTag("Monster");
        Manager.SetMonster(monsterObj);
    }

    [PunRPC]
    void RPC_NavigatorInit()
    {
        Debug.Log("navigator please");
        navigatorObj = GameObject.FindGameObjectWithTag("Player");
        Manager.SetNavigator(navigatorObj);
    }
}
