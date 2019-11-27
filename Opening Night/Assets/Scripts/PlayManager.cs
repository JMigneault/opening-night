using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Manages the state of play by switching between phases and enabling/disabling gameobjects and UI.
 */
public class PlayManager : MonoBehaviour
{
    // phase trackers
    [SerializeField] private GameTimer timer;
    [SerializeField] private PhaseManager phaseManager;

    // cameras for different game phases
    [SerializeField] private GameObject placeCamera;
    [SerializeField] private GameObject playCamera;

    // player characters
    private GameObject navigator;
    private GameObject monster;
    private Vector2 navInitPos;
    private Vector2 monInitPos;

    // properties to be set
    [SerializeField] private float placeTime;

    [SerializeField] private ObjectGrid objectGrid;
    private bool foundKey = false;

    [SerializeField] private Canvas placementUI;

    [SerializeField] private Chest[] chests;
    private bool doorsOpen = false;
    public bool DoorsOpen { get { return doorsOpen; } }

    // initially set up game to place phase
    void Start()
    {
        this.placeCamera.SetActive(true);
        this.playCamera.SetActive(false);
        AddKey();
    }

    void Update()
    {
        // if place timer has run out
        if (this.IsPlacementDone())
        {
            Debug.Log("placing done");
            this.SwitchToPlay();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("placing ended by player");
            this.SwitchToPlay();
        }
    }

    public void SetMonster(GameObject mon)
    {
        Debug.Log("Monster set");
        monster = mon;
        monInitPos = mon.transform.position;
}

    public void SetNavigator(GameObject nav)
    {
        Debug.Log("Navigator set");
        navigator = nav;
        navInitPos = navigator.transform.position;
    }

    /**
     * Switch game to play mode (resetting player positions, enabling play camera, etc.)
     */
    void SwitchToPlay()
    {
        if (phaseManager.CurrentPhase == Phase.Play)
        {
            Debug.Log("WARNING (SwitchToPlay): Already in playing phase.");
        } else
        {
            phaseManager.SwitchToPlay();
            this.placeCamera.SetActive(false);
            this.playCamera.SetActive(true);
            this.navigator.transform.position = this.navInitPos;
            // this.navigator.GetComponent<Player>().RestoreSpeed(); todo: fix bug where permanent movement drop carries over to next game
            this.monster.transform.position = this.monInitPos;
            this.navigator.SetActive(true);
            this.monster.SetActive(true);
            this.placementUI.enabled = false;
        }

    }

    public void OpenDoors()
    {
        doorsOpen = true;
    }

    /**
     * Switch game to place mode (disabling players, enabling place camera, etc.)
     */
    public void SwitchToPlace()
    {
        if (phaseManager.CurrentPhase == Phase.Place)
        {
            Debug.Log("WARNING (SwitchToPlace): Already in placing phase.");
        } else
        {
            RemoveKey();
            AddKey();
            phaseManager.SwitchToPlace();
            this.playCamera.SetActive(false);
            this.placeCamera.SetActive(true);
            this.navigator.GetComponent<PlayerMovement>().resetSpeed();
            this.navigator.SetActive(false);
            this.monster.GetComponent<MonsterMovement>().resetSpeed();
            this.monster.SetActive(false);
            this.placementUI.enabled = true;
        }
    }

    /**
     * Check if placement time has run out.
     */
    private bool IsPlacementDone()
    {
        return this.phaseManager.CurrentPhase == Phase.Place && timer.PhaseTime > placeTime;
    }

    public void AddKey()
    {
        //chests[Random.Range(0, chests.Length)].SetToHaveKey();
    }

    public void RemoveKey()
    {
        //objectGrid.DeleteCellObject(this.key.coords);
        // key.DeleteSelf(objectGrid);
    }


}
