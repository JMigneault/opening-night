using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class PlayerLightRadius : MonoBehaviour {

    public Player player;
    private Light lt;

    [SerializeField] private float maxRange; // 20
    [SerializeField] private float minRange; // 3
    [SerializeField] private float lightDecayRate; // .25
    [SerializeField] private float lightDecayConstant; // 10/4
    [SerializeField] private float lightGrowthRate; // 20
    [SerializeField] private float lightGrowthDelay;

    private float chargeTime = 0.0f;

    private bool shouldDecay = true;
    public bool ShouldDecay { set { shouldDecay = value; } }

    private void Start()
    {
        this.lt = GetComponent<Light>();
    }

    public float GetRange()
    {
        return this.lt.range - minRange;
    }

    public void AdjustRange(float amount)
    {
        this.lt.range = Mathf.Clamp(this.lt.range + amount, minRange, maxRange);
    }

    public void SetColor(Color color)
    {
        lt.color = color;
    }

    private void UpdatePosition()
    {
        Vector2 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + .6f, transform.position.z);
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.PlayerLight = this;
        }
        else
        {
            if(Input.GetKey(KeyCode.Space))
            {
                player.GetComponent<PlayerMovement>().SetCanMove(false);
                if (chargeTime > lightGrowthDelay)
                {
                    AdjustRange(Time.deltaTime * lightGrowthRate);
                }
                chargeTime += Time.deltaTime;
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                chargeTime = 0.0f;
                player.GetComponent<PlayerMovement>().SetCanMove(true);
            }
            else if (shouldDecay)
            {
                AdjustRange(-1 * Time.deltaTime * (this.lt.range * lightDecayRate + lightDecayConstant));
            }
            UpdatePosition();
        }
    }
}
