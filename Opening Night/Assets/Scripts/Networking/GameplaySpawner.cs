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
    private GameObject GameManager;

    private GameObject monsterObj;
    private GameObject navigatorObj;

    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if(PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            monsterObj = PhotonNetwork.Instantiate(Path.Combine("GamePrefabs", "Monster"), monsterSpawn.position, monsterSpawn.rotation, 0);
            GameManager.GetComponent<PlayManager>().SetMonster(monsterObj);
            PV.RPC("RPC_MonsterInit", RpcTarget.Others);
        }
        else
        {
            navigatorObj = PhotonNetwork.Instantiate(Path.Combine("GamePrefabs", "Player"), playerSpawn.position, playerSpawn.rotation, 0);
            GameManager.GetComponent<PlayManager>().SetNavigator(navigatorObj);
            PV.RPC("RPC_NavigatorInit", RpcTarget.Others);
        }
    }

    [PunRPC]
    void RPC_MonsterInit()
    {
        Debug.Log("monster please");
        monsterObj = GameObject.FindGameObjectWithTag("Monster");
        GameManager.GetComponent<PlayManager>().SetMonster(monsterObj);
    }

    [PunRPC]
    void RPC_NavigatorInit()
    {
        Debug.Log("navigator please");
        navigatorObj = GameObject.FindGameObjectWithTag("Player");
        GameManager.GetComponent<PlayManager>().SetNavigator(navigatorObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
