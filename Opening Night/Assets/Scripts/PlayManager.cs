using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GameObject navigator;
    [SerializeField] private GameObject monster;
    private Vector2 navInitPos;
    private Vector2 monInitPos;

    // properties to be set
    [SerializeField] private float placeTime;

    // initially set up game to place phase
    void Start()
    {
        this.placeCamera.SetActive(true);
        this.playCamera.SetActive(false);
        this.navInitPos = navigator.transform.position;
        this.monInitPos = monster.transform.position;
        this.navigator.SetActive(false);
        this.monster.SetActive(false);
    }

    void Update()
    {
        // if place timer has run out
        if (this.IsPlacementDone())
        {
            Debug.Log("placing done");
            this.SwitchToPlay();
        }
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
            this.monster.transform.position = this.monInitPos;
            this.navigator.SetActive(true);
            this.monster.SetActive(true);
        }

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
            phaseManager.SwitchToPlace();
            this.playCamera.SetActive(false);
            this.placeCamera.SetActive(true);
            this.navigator.SetActive(false);
            this.monster.SetActive(false);
        }
    }

    /**
     * Check if placement time has run out.
     */
    private bool IsPlacementDone()
    {
        return this.phaseManager.CurrentPhase == Phase.Place && timer.PhaseTime > placeTime;
    }

}
