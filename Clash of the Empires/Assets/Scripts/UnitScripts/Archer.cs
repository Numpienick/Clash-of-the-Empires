﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : OffensivePlaceables
{
    public GameObject Bullet;
    public GameObject Bullet_Emitter;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
       // Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 180);

        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        Destroy(Temporary_Bullet_Handler, 10.0f);
    }
}
