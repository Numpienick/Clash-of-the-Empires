using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Units : OffensivePlaceables
{
    private SphereCollider detectingRange;
    [HideInInspector]
    public NavMeshAgent agent;
    public Collider targetCollider;
    public CheckForEnemy checkForEnemyRef;
    public bool followTarget = true;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectingRange = GetComponent<SphereCollider>();
        healthbarOffsetY = 6;
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
            FollowTarget(checkForEnemyRef.mainTarget);

        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(6);

        if (canvas != null)
        {
            UpdateHealthBarPosition();
            healthFill.value = currentHealth / maxHealth;
        }
    }

    void DealDamage(float damageValue)
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
