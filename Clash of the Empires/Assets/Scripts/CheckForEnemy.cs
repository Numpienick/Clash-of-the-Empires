using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CheckForEnemy : MonoBehaviour
{
    public OffensivePlaceables offensivePlaceablesRef;
    public GameObject mainTarget;
    //[HideInInspector]
    public bool readyToShoot = false;

    List<Placeables> enemyUnit = new List<Placeables>(0);
    Placeables[] unitsFound;

    Placeables mainEnemy;

    // Use this for initialization
    void Awake()
    {
        offensivePlaceablesRef = transform.root.GetComponent<OffensivePlaceables>();
    }

    /*public void OnTriggerEnter(Collider other)
    {
        OffensivePlaceables enemyUnits = other.transform.root.GetComponent<OffensivePlaceables>();
        if(enemyUnits != null)
        enemyUnits.followTarget = true;
    }*/

    private void Update()
    {
        //Debug.Log(mainEnemy + " is target van " + offensivePlaceablesRef.name);
        //if(mainEnemy != null)
        //Debug.Log("noEnemy " + offensivePlaceablesRef.name + " " + mainEnemy.name);
        if (enemyUnit.Count > 0)
        {
            mainEnemy = enemyUnit[0];

            if (mainEnemy == null)
            {
                readyToShoot = false;
                offensivePlaceablesRef.followTarget = false;
                enemyUnit.RemoveAt(0);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        unitsFound = other.transform.root.GetComponents<Placeables>();

        foreach (Placeables unit in unitsFound)
        {
            enemyUnit.Add(unit);
        }

        for (int i = 0; i < enemyUnit.Count; i++)
        {
            if (enemyUnit[i].currentTeam == offensivePlaceablesRef.currentTeam)
            {
                enemyUnit.RemoveAt(i);
            }
        }

        if (mainEnemy != null && offensivePlaceablesRef.currentTeam != mainEnemy.currentTeam)
        {
            offensivePlaceablesRef.followTarget = true;
            readyToShoot = true;
            mainTarget = mainEnemy.gameObject;
            offensivePlaceablesRef.target = mainTarget;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (mainEnemy != null && offensivePlaceablesRef.currentTeam != mainEnemy.currentTeam)
        {
            mainTarget = null;
        }
    }
}

