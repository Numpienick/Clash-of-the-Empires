using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barbarian : OffensivePlaceables
{

    public Vector3 debugPos;
    public Vector3 targetVector3;

    // Use this for initialization
    void Start()
    {
        healthbarOffsetY = 6;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(debugPos, targetVector3);
        killButton();
        checkRange();
    }

    void checkRange()
    {
        
        Collider[] units =

            Physics.OverlapSphere(transform.position, visionRange);

        foreach (Collider nearbyUnit in units)
        {
            Units enemyTeamRef = nearbyUnit.GetComponent<Units>();
            
            if (enemyTeamRef.currentTeam != this.currentTeam)
            {
                targetVector3 = nearbyUnit.transform.position;
                this.GetComponent<NavMeshAgent>().SetDestination(targetVector3);
            }
        }
    }

    void attackTarget()
    {
      
        this.GetComponent<NavMeshAgent>().SetDestination(targetVector3);
    }

    void killButton()
    {
        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(6);

        UpdateHealthBarPosition();
        healthFill.value = currentHealth / maxHealth;
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
