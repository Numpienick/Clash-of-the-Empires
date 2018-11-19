using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barbarian : OffensivePlaceables
{
    Sword swordScript;

    //Setting variables and getting components
    public override void Start()
    {
        base.Start();
        fireRate = 0.9f;
        damage = 20;
        agent.stoppingDistance = 9;
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
        swordScript = GetComponentInChildren<Sword>();
    }

    public override void Update()
    {
        base.Update();
        //Attacking according to the fireRate
        if (checkForEnemyRef.readyToShoot == true && followTarget == true && Time.time >= nextTimeToFire && swordScript.attackReady == true)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Attack();
        }
    }

    void Attack()
    {
        //Call the fucntion that'll deal damage
        swordScript.Attack(damage);
    }
}