using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CheckForEnemy : MonoBehaviour
{
    [HideInInspector]
    public Placeables offensivePlaceablesRef;

    public bool readyToShoot = false;

    //[HideInInspector]
    public List<Placeables> enemies = new List<Placeables>(0);

    [HideInInspector]
    public Placeables enemy = null;

    Placeables[] targets = null;

    // Use this for initialization
    void Awake()
    {
        offensivePlaceablesRef = transform.root.GetComponent<Placeables>();
    }

    private void Update()
    {
        for (int i = 0; i < enemies.Count;)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }

            else
                i++;
        }

        if (enemies.Count > 0)
        {
            enemy = enemies[0];
        }

        if (enemy == null && offensivePlaceablesRef != null)
        {
            readyToShoot = false;
            offensivePlaceablesRef.moveToTarget = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckForEnemy" || other.tag == "Goldmine")
            targets = other.GetComponentsInParent<Placeables>();

        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].currentTeam != offensivePlaceablesRef.currentTeam && enemies.Contains(targets[i]) == false)
                {
                    enemy = targets[i];
                    enemies.Add(enemy);
                    enemy = enemies[0];
                }
            }

            if (enemy != null && offensivePlaceablesRef.currentTeam != enemy.currentTeam)
            {
                if (offensivePlaceablesRef.turret == false)
                {
                    offensivePlaceablesRef.moveToTarget = true;
                }
                readyToShoot = true;
                offensivePlaceablesRef.target = enemy;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Placeables target = null;
        if (other.tag == "CheckForEnemy")
        {
            target = other.GetComponent<Placeables>();
        }

        if (target != null && target.currentTeam != offensivePlaceablesRef.currentTeam)
        {
            enemies.Remove(target);
            offensivePlaceablesRef.moveToTarget = false;
            enemy = null;
        }
    }
}