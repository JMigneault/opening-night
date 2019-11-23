using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }

    private void Update()
    {
        if (isSearching)
        {
            timeSearched += Time.deltaTime;
            if (timeSearched > searchTime)
            {
                OpenChest();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartSearch();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EndSearch();
        }
    }

}
