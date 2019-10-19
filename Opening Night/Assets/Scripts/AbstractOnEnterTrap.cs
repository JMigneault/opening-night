using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class AbstractOnEnterTrap : AbstractTrap
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateTrap(collision.GetComponent<Player>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndTrap(collision.GetComponent<Player>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DuringTrap(collision.GetComponent<Player>());
        }
    }

    abstract protected void ActivateTrap(Player player);

    abstract protected void EndTrap(Player player);

    abstract protected void DuringTrap(Player player);

}
