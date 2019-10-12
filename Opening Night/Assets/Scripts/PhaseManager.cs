using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// represents a game phase
public enum Phase
{
    Place = 0,
    Play = 1
}

/**
 * Manages the current phase.
 */
public class PhaseManager : MonoBehaviour
{
    // tracks game time
    [SerializeField] private GameTimer timer;

    private Phase currentPhase = Phase.Place;
    public Phase CurrentPhase { get { return currentPhase; } }

    // switch to the placing phase
    public void SwitchToPlace()
    {
        this.currentPhase = Phase.Place;
        timer.NewPhase();
    }

    // switch to the playing phase
    public void SwitchToPlay()
    {
        this.currentPhase = Phase.Play;
        timer.NewPhase();
    }

}
