using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEnemy : Units
{
    public Barbarian unitsRef;
    public GameObject mainTarget;

    // Use this for initialization
    void Start()
    {
        unitsRef = transform.root.GetComponent<Barbarian>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Units enemyUnit = other.GetComponent<Units>();
        if (enemyUnit.currentTeam != unitsRef.currentTeam)
        {
            unitsRef.FollowTarget(mainTarget);
        }
    }
}
