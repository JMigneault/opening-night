using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class AbstractOnEnterTrap : AbstractTrap
{

    [SerializeField] private float triggerTime;
    private SpriteRenderer spriteRenderer;
    private bool activated = false;

    private float collisionTime = 0.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.GetInt("IsNavigator") == 1)
        {
            spriteRenderer.enabled = false;
        } // todo: give monster way to see if traps have popped
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated && collision.CompareTag("Player"))
        {
            ActivateTrap(collision.GetComponent<Player>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (activated)
            {
                DuringTrap(collision.GetComponent<Player>());
            }
            else
            {
                spriteRenderer.enabled = true;
                collisionTime += Time.deltaTime;
                if (collisionTime > triggerTime)
                {
                    activated = true;
                    ActivateTrap(collision.GetComponent<Player>());
                    if (PlayerPrefs.GetInt("IsNavigator") == 0)
                    {
                        spriteRenderer.color = new Color(255, 255, 255, .2f);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (activated)
            {
                EndTrap(collision.GetComponent<Player>());
            } else
            {
                collisionTime = 0.0f;
                if (PlayerPrefs.GetInt("IsNavigator") == 1)
                {
                    spriteRenderer.enabled = false;
                }
            }
        }
    }

    abstract protected void ActivateTrap(Player player);

    abstract protected void EndTrap(Player player);

    abstract protected void DuringTrap(Player player);

}
