using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CheckForEnemy : MonoBehaviour
{
    public OffensivePlaceables offensivePlaceablesRef;
    public GameObject mainTarget;

    // Use this for initialization
    void Awake()
    {
        offensivePlaceablesRef = transform.root.GetComponent<OffensivePlaceables>();
    }

    public void OnTriggerEnter(Collider other)
    {
        OffensivePlaceables enemyUnit = other.transform.root.GetComponent<OffensivePlaceables>();

        if (enemyUnit != null && offensivePlaceablesRef.currentTeam != enemyUnit.currentTeam)
        {
            mainTarget = enemyUnit.gameObject;
            offensivePlaceablesRef.followTarget = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        OffensivePlaceables enemyUnit = other.transform.root.GetComponent<OffensivePlaceables>();
        if (enemyUnit != null && offensivePlaceablesRef.currentTeam != enemyUnit.currentTeam)
        {
            mainTarget = null;
        }
    }
}
