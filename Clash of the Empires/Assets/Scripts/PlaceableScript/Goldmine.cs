using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldmine : MonoBehaviour
{
    public Player playerRef;
    private GameObject player;

    private float timeAlive = 60;
    private float timeToSpawn = 5;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRef = player.transform.root.GetComponent<Player>();
        InvokeRepeating("GenerateMoney", 5f, timeToSpawn);
    }

    void Update()
    {
        Destroy(gameObject, timeAlive);
    }

    void GenerateMoney()
    {
        playerRef.money +=250;
    }
}
