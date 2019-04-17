using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffensivePlaceables : Placeables
{
    public Collider targetCollider;

    [HideInInspector]
    public Rigidbody rb;

    public float nextTimeToFire = 0f;

    public float damage = 0;
    public float fireRate = 15f;

    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Start()
    {
        base.Start();
        rb = GetComponentInChildren<Rigidbody>();
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
    }

    public override void Update()
    {
        base.Update();
        if (moveToTarget == true && turret == false)
            FollowTarget(target);
    }

    public void FollowTarget(Placeables target)
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            targetPosition.y = this.transform.position.y;
            transform.LookAt(targetPosition);

            float distance = Vector3.Distance(target.transform.position, this.transform.position);

            if (distance > 3 && target.currentHealth > 0 && currentHealth > 0)
            {
                agent.SetDestination(targetPosition);
                this.GetComponent<NavMeshAgent>().speed = 20;
            }
        }
    }
}