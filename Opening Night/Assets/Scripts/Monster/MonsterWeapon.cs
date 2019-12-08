using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(BoxCollider2D))]
public class MonsterWeapon : MonoBehaviour
{

    [SerializeField] private float freezeTime;

    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("hit player");
            collision.GetComponent<Player>().RestrictMovement(freezeTime);
            PV.RPC("PlayerHit", RpcTarget.Others);
        }
    }

    [PunRPC]
    void PlayerHit()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().RestrictMovement(freezeTime);
    }

}
