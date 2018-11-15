using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldmine : Placeables
{
    public Player playerRef;
    private GameObject player;

    private float timeAlive = 60;
    private float timeToSpawn = 5;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        healthbarOffsetY = 10f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerRef = player.transform.root.GetComponent<Player>();
        InvokeRepeating("GenerateMoney", 5f, timeToSpawn);
    }

    public override void Update()
    {
        base.Update();
        Destroy(gameObject, timeAlive);
    }

    void GenerateMoney()
    {
        playerRef.money +=250;
    }
}
