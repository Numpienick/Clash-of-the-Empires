using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffensivePlaceables : Placeables
{
    public Collider targetCollider;
    public bool followTarget = true;

    public float damage = 0;
    public float visionRange = 0;
    public float shootingRange = 0;
    public float fireRate = 0;

    [HideInInspector]
    public GameObject target;

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Start()
    {
        base.Start();
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
    }

    public override void Update()
    {
        base.Update();
        if (followTarget)
            FollowTarget(target);

        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(6);
    }

    public void DealDamage(float damageValue)
    {
        currentHealth -= damageValue;
        Debug.Log("ouch");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void FollowTarget(GameObject target)
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            transform.LookAt(targetPosition);

            float distance = Vector3.Distance(target.transform.position, this.transform.position);

            if (distance > 5)
            {
                agent.SetDestination(targetPosition);
                this.GetComponent<NavMeshAgent>().speed = 30;
            }
        }
    }

}
