﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : OffensivePlaceables
{
    public GameObject Bullet;
    public GameObject Bullet_Emitter;
    public Transform arrowHolder;
    

    public override void Start()
    {
        base.Start();
        damage = 20f;
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
    }

    public override void Update()
    {
        base.Update();
        if (checkForEnemyRef.mainTarget != null)
        {
            Bullet_Emitter.transform.LookAt(checkForEnemyRef.mainTarget.transform.Find("LookAtMe"));
        }

        if (Input.GetKey("h"))
        {
            Shoot();
        }

        if (checkForEnemyRef.readyToShoot == true && followTarget == true && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }       
    }
    public void Shoot()
    {
        GameObject Temporary_Bullet_Handler;

        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation, arrowHolder) as GameObject;
        Destroy(Temporary_Bullet_Handler, 10.0f);
    }
}

