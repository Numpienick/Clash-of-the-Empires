using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffensivePlaceables : Placeables
{
    [HideInInspector]
    public NavMeshAgent agent;
    public Collider targetCollider;
    public CheckForEnemy checkForEnemyRef;
    public bool followTarget = true;

    public float damage = 0;
    public float visionRange = 0;
    public float shootingRange = 0;
    public float fireRate = 0;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
    }

    void Update()
    {
        if (followTarget)
            FollowTarget(checkForEnemyRef.mainTarget);

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
