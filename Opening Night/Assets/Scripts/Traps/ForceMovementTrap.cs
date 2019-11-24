using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMovementTrap : AbstractOnEnterTrap
{
    [SerializeField] private float slideSpeed = 100;
    private Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    public int directionIndex = 1;

    private Rigidbody2D rigid;

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
        rigid.velocity = Vector3.zero;
        rigid.AddForce(slideSpeed * directions[directionIndex]);
        player.RestrictMovement(.5f);
    }

    protected override void EndTrap(Player player)
    {
        // no effect
    }

    protected override void DuringTrap(Player player)
    {
        rigid.AddForce((slideSpeed / 13f) * directions[directionIndex]);
    }

    public override void Rotate()
    {
        Debug.Log("rotate");
        if (directionIndex == 3)
        {
            directionIndex = 0;
        } else
        {
            directionIndex++;
        }
    }
}
