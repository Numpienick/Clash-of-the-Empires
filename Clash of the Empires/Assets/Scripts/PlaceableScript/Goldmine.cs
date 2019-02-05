using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldmine : Placeables
{
    private Player playerRef;
    private AIOpponent AIRef;

    private float timeAlive = 20;
    private float timeToSpawn = 5;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        healthbarOffsetY = 10f;
        switch (currentTeam)
        {
            case (1):
                playerRef = FindObjectOfType<Player>();
                InvokeRepeating("GenerateMoney", 5f, timeToSpawn);
                break;

            case (2):
                AIRef = FindObjectOfType<AIOpponent>();
                InvokeRepeating("GenerateMoney", 5f, timeToSpawn);
                break;
        }
        
    }

    public override void Update()
    {
        base.Update();
        Destroy(gameObject, timeAlive);
    }

    void GenerateMoney()
    {
        switch (currentTeam)
        {
            case (1):
                playerRef.money += 250;
                break;

            case (2):
                Debug.Log("giving money");
                AIRef.money += 250;
                break;
        }        
    }
}
