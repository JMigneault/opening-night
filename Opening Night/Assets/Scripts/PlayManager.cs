using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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

    [SerializeField] private Canvas placementUI;
    [SerializeField] private CountdownOverlay countdownOverlay;

    [SerializeField] private Chest[] chests;

    [SerializeField] private GameObject lineRenderer;

    [SerializeField] private Text timerText;
    private int timeLeft;
    
    private bool doorsOpen = false;
    public bool DoorsOpen { get { return doorsOpen; } }

    private PhotonView pv;

    // initially set up game to place phase
    void Start()
    {
        pv = GetComponent<PhotonView>();
        this.placeCamera.SetActive(true);
        if (PlayerPrefs.GetInt("IsNavigator") == 1)
        {
            countdownOverlay.gameObject.SetActive(true);
            countdownOverlay.SetText("Waiting for placement");
        } else
        {
            countdownOverlay.gameObject.SetActive(false);
            timeLeft = (int)placeTime;
            timerText.text = timeLeft + " seconds";
            Invoke("TimerTick", 1f);
        }
        this.playCamera.SetActive(false);
        
        
        if (PlayerPrefs.GetInt("IsNavigator") == 0)
        {
            AddKey();
        }
    }

    void Update()
    {
        // if place timer has run out
        if (PlayerPrefs.GetInt("IsNavigator") == 0 && (this.IsPlacementDone() || Input.GetKeyDown(KeyCode.Space)))
        {
            this.SwitchToPlay();
            pv.RPC("SwitchToPlay", RpcTarget.Others);
        }
    }

    [PunRPC]
    void RPC_SpacePressed()
    {
        Debug.Log("placing ended by player");
        this.SwitchToPlay();
    }

    public void SetMonster(GameObject mon)
    {
        Debug.Log("Monster set");
        monster = mon;
        monInitPos = mon.transform.position;
        this.monster.GetComponent<MonsterMovement>().ResetSpeed();
        this.monster.SetActive(false);
    }

    public void SetNavigator(GameObject nav)
    {
        Debug.Log("Navigator set");
        navigator = nav;
        navInitPos = navigator.transform.position;
        this.navigator.GetComponent<PlayerMovement>().ResetSpeed();
        this.navigator.SetActive(false);
    }

    private IEnumerator SwitchToPlayCoroutine()
    {
        countdownOverlay.gameObject.SetActive(true);
        phaseManager.SwitchToPlay();
        countdownOverlay.SetText("Game starts in 3...");
        yield return new WaitForSeconds(1.0f);
        countdownOverlay.SetText("Game starts in 2...");
        yield return new WaitForSeconds(1.0f);
        countdownOverlay.SetText("Game starts in 1...");
        yield return new WaitForSeconds(1.0f);
        this.placeCamera.SetActive(false);
        this.playCamera.SetActive(true);
        this.navigator.transform.position = this.navInitPos;
        // this.navigator.GetComponent<Player>().ResetSpeed() // todo: fix bug where permanent movement drop carries over to next game
        this.monster.transform.position = this.monInitPos;
        this.navigator.SetActive(true);
        this.monster.SetActive(true);
        this.placementUI.enabled = false;
        countdownOverlay.gameObject.SetActive(false);
        lineRenderer.SetActive(false);
        timeLeft = -1;
    }

    /**
     * Switch game to play mode (resetting player positions, enabling play camera, etc.)
     */
    [PunRPC]
    void SwitchToPlay()
    {
        if (phaseManager.CurrentPhase == Phase.Play)
        {
            Debug.Log("WARNING (SwitchToPlay): Already in playing phase.");
        } else
        {
            StartCoroutine(SwitchToPlayCoroutine());
        }
    }

    public void OpenDoors()
    {
        this.doorsOpen = true;
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
            //PlayerPrefs.SetInt("IsNavigator", 1 - PlayerPrefs.GetInt("IsNavigator"));
            RemoveKey();
            AddKey();
            phaseManager.SwitchToPlace();
            this.playCamera.SetActive(false);
            this.placeCamera.SetActive(true);
            this.navigator.GetComponent<PlayerMovement>().ResetSpeed();
            this.navigator.SetActive(false);
            this.monster.GetComponent<MonsterMovement>().ResetSpeed();
            this.monster.SetActive(false);
            this.placementUI.enabled = true;
            timeLeft = (int)placeTime;
            timerText.text = timeLeft + " seconds";
            Invoke("TimerTick", 1f);
            if(PlayerPrefs.GetInt("IsNavigator") == 1)
            {
                countdownOverlay.gameObject.SetActive(true);
                countdownOverlay.SetText("Waiting for placement phase.");
            }
            lineRenderer.SetActive(true);
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Line"))
            {
                Destroy(go);
            }
        }
    }

    private void TimerTick()
    {
        timeLeft--;
        timerText.text = timeLeft + " seconds";
        if(timeLeft > 0)
        {
            Invoke("TimerTick", 1f);
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
        Random.InitState((int)System.DateTime.Now.Ticks);
        int index = Random.Range(0, chests.Length);
        Debug.Log(index);
        pv.RPC("AddKey", RpcTarget.All, index);
    }

    [PunRPC]
    public void AddKey(int index)
    {
        chests[index].SetToHaveKey();
    }

    public void RemoveKey()
    {
        //objectGrid.DeleteCellObject(this.key.coords);
        // key.DeleteSelf(objectGrid);
    }

}
