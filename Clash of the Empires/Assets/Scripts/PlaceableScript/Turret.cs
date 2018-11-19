using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Turret : Placeables {

    public Player playerRef;
    private GameObject player;
    public Collider turretRange;
    public float damage = 5f;
    public float shockRate = 0.6f;
    public GameObject lightningStrike;
    public OffensivePlaceables target;
    public GameObject shockingGO;
    public bool striking = false;

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRef = player.transform.root.GetComponent<Player>();
        lightningStrike.SetActive(false);
        striking = false;
    }

    void OnTriggerStay(Collider other)
    {
        target = other.GetComponent<OffensivePlaceables>();
        //shockingGO = other;
        if (target.currentTeam != playerRef.currentTeam)
        {
            Electrocute();
        }
    }


    void Electrocute()
    {
        if (striking == false)
        {
            striking = true;
            lightningStrike.SetActive(true);
            //lightningStrike.GetComponent<LightningBoltScript>().EndObject = shockingGO;
            InvokeRepeating("Shock", 0.6f, shockRate);
        }
    }

    void Shock()
    {
        target.DealDamage(damage);
    }
	
}
