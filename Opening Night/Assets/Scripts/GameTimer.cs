using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Tracks time for the game and current phase.
 */
public class GameTimer : MonoBehaviour
{

    // amount of time passed in the phase
    private float phaseTime = 0.0f;
    public float PhaseTime { get { return phaseTime; } }

    // amount of time passed in the game
    private float gameTime = 0.0f;
    public float GameTime { get { return gameTime; } }

    // add time to timers on each frame
    void Update()
    {
        this.phaseTime += Time.deltaTime;
        this.gameTime += Time.deltaTime;
    }

    // start a new phase
    public void NewPhase()
    {
        this.phaseTime = 0.0f;
    }
    
}
