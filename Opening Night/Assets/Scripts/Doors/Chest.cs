using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Chest : MonoBehaviour
{
    [SerializeField] private bool containsKey;
    [SerializeField] private float searchTime;
    [SerializeField] PlayManager playManager;

    private bool opened = false;
    private float timeSearched = 0.0f;
    private bool isSearching = false;


    public void SetToHaveKey()
    {
        containsKey = true;
    }

    private void StartSearch()
    {
        isSearching = true;
    }

    private void EndSearch()
    {
        timeSearched = 0.0f;
        isSearching = false;
    }

    private void OpenChest()
    {
        opened = true;
        if (containsKey)
        {
            playManager.OpenDoors();
            GetComponent<SpriteRenderer>().color = Color.green;
        } else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void Update()
    {
        if (isSearching && !opened)
        {
            timeSearched += Time.deltaTime;
            if (timeSearched > searchTime)
            {
                OpenChest();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("trigger");
            StartSearch();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EndSearch();
        }
    }

}
