using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Units : OffensivePlaceables
{
    private SphereCollider detectingRange;
    [HideInInspector]
    public NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectingRange = GetComponent<SphereCollider>();
        healthbarOffsetY = 6;
    }

    // Update is called once per frame
    void Update()
    {
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
}
