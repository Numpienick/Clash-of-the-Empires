using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CheckForEnemy : MonoBehaviour
{
    [HideInInspector]
    public Placeables offensivePlaceablesRef;

    public bool readyToShoot = false;

    [HideInInspector]
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
        //Removes units from the list if they have died
        for (int i = 0; i < enemies.Count;)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }

            else
                i++;
        }

        //The targeted enemy is always the first enemy the unit sees
        if (enemies.Count > 0)
        {
            enemy = enemies[0];
        }

        //Stops the enemy from shooting and moving towards a target when there is no target
        if (enemy == null && offensivePlaceablesRef != null)
        {
            readyToShoot = false;
            offensivePlaceablesRef.moveToTarget = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckForEnemy" || other.tag == "Goldmine") //Goldmine doesn't use the CheckForEnemy collider so it has a different tag
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
                //If the unit isn't a turret, move towards the target (turrets cannot move ofcourse)
                if (offensivePlaceablesRef.turret == false)
                {
                    offensivePlaceablesRef.moveToTarget = true;
                }
                readyToShoot = true;
                offensivePlaceablesRef.target = enemy;
            }
        }
    }

    //When an enemy gets away from a unit, delete that enemy from the enemies list
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