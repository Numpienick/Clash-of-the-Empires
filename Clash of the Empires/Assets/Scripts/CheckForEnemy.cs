using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CheckForEnemy : MonoBehaviour
{
    private Units unitsRef;
    public GameObject mainTarget;

    // Use this for initialization
    void Awake()
    {
        unitsRef = transform.root.GetComponent<Units>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Units enemyUnit = other.transform.root.GetComponent<Units>();

        if (enemyUnit != null && unitsRef.currentTeam != enemyUnit.currentTeam)
        {
            mainTarget = enemyUnit.gameObject;
            unitsRef.followTarget = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Units enemyUnit = other.transform.root.GetComponent<Units>();
        if (enemyUnit != null && unitsRef.currentTeam != enemyUnit.currentTeam)
        {
            mainTarget = null;
        }
    }
}
