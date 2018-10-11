using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEnemy : Units
{
    private Barbarian unitsRef;
    public GameObject mainTarget;

    // Use this for initialization
    void Awake()
    {
        unitsRef = transform.root.GetComponent<Barbarian>();
    }

    public void OnTriggerStay(Collider other)
    {
        Units enemyUnit = other.transform.root.GetComponent<Units>();

        if (enemyUnit != null && unitsRef.currentTeam != enemyUnit.currentTeam)
        {
            Debug.Log("YourTeam" + unitsRef.currentTeam);
            Debug.Log("EnemyTeam" + enemyUnit.currentTeam);
            mainTarget = enemyUnit.gameObject;
            unitsRef.FollowTarget(mainTarget);
        }
    }
}
