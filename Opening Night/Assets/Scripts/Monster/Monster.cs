using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Monster : MonoBehaviour
{

    private PhotonView pv;

    [SerializeField] private MonsterWeapon attackObject;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackDuration;

    private Vector2 defaultDirection = Vector2.down;

    MonsterMovement movement;
    
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        movement = GetComponent<MonsterMovement>();
        attackObject.gameObject.SetActive(false);
    }

    [PunRPC]
    private void FreezeAttack()
    {
        Vector2 input = movement.GetInput();
        float angle = Vector2.SignedAngle(defaultDirection, input);
        attackObject.transform.RotateAround(transform.position, new Vector3(0, 0, 1), angle);
        StartCoroutine(FreezeAttackCoroutine(-1 * angle));
    }

    private IEnumerator FreezeAttackCoroutine(float resetAngle)
    {
        movement.SetCanMove(false);
        yield return new WaitForSeconds(attackDelay);
        attackObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackObject.gameObject.SetActive(false);
        attackObject.transform.RotateAround(transform.position, Vector3.forward, resetAngle);
        movement.SetCanMove(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            FreezeAttack();
            pv.RPC("FreezeAttack", RpcTarget.Others);
        }
    }

}
