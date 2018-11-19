using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Archer : OffensivePlaceables
{
    public GameObject Bullet;
    public GameObject Bullet_Emitter;
    public Transform arrowHolder;

    //setting and getting variables
    public override void Start()
    {
        base.Start();
        damage = 20f;
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
    }

    public override void Update()
    {
        base.Update();
        //adjusting the stopping distance of the Archer according to which "mode" it's in
        switch (checkForEnemyRef.readyToShoot)
        {
            case (true):
                agent.stoppingDistance = 40;
                break;

            case (false):
                agent.stoppingDistance = 10;
                break;
        }
        //Finding where the archer needs to aim at
        if (checkForEnemyRef.enemy != null)
        {
            Bullet_Emitter.transform.LookAt(checkForEnemyRef.enemy.transform.Find("LookAtMe"));
        }

        //Manually shooting for debugging purposes
        /*f (Input.GetKey("h"))
         {
             Shoot();
         }*/

        //letting the archer shoot according to it's fire rate and whether or not it has found an enemy
        if (checkForEnemyRef.readyToShoot == true && followTarget == true && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    //Instantiate an arrow and destory it after 10 seconds
    public void Shoot()
    {
        GameObject Temporary_Bullet_Handler;

        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation, arrowHolder) as GameObject;
        Destroy(Temporary_Bullet_Handler, 10.0f);
    }
}

