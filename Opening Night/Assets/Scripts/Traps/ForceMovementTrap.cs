using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMovementTrap : AbstractOnEnterTrap
{
    [SerializeField] private float slideSpeed = 100;
    [SerializeField] private Vector2 direction;

    private Rigidbody2D rigid;

    // 0 is right, 1 is forward, 2 is left, 3 is back
    //[SerializeField] private int direction = 1;

    public override TrapType GetTrapType()
    {
        return TrapType.ForceMovement;
    }

    protected override void ActivateTrap(Player player)
    {
        if(!rigid)
        {
            rigid = player.gameObject.GetComponent<Rigidbody2D>();
        }
        //forces player to slide in a direction
        //player.Slide(slideSpeed, direction);
        //player.gameObject.transform.position = transform.position;
        
        rigid.velocity = Vector3.zero;
        rigid.AddForce(slideSpeed * direction);
        player.RestrictMovement(.5f);
    }

    protected override void EndTrap(Player player)
    {
        // no effect
    }

    protected override void DuringTrap(Player player)
    {
        // no effect
        rigid.AddForce((slideSpeed / 13f) * direction);
    }
}
