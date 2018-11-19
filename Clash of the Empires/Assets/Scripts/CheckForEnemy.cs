using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CheckForEnemy : MonoBehaviour
{
    [HideInInspector]
    public OffensivePlaceables offensivePlaceablesRef;

    [HideInInspector]
    public bool readyToShoot = false;

    [HideInInspector]
    public List<OffensivePlaceables> enemies = new List<OffensivePlaceables>(0);

    [HideInInspector]
    public OffensivePlaceables enemy = null;

    OffensivePlaceables[] targets = null;

    public bool moveToTarget;

    // Use this for initialization
    void Awake()
    {
        offensivePlaceablesRef = transform.root.GetComponent<OffensivePlaceables>();
    }

    private void Update()
    {
        if (enemy == null)
        {
            readyToShoot = false;
            moveToTarget = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckForEnemy")
            targets = other.GetComponentsInParent<OffensivePlaceables>();

        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].currentTeam != offensivePlaceablesRef.currentTeam)
                {
                    enemy = targets[i];
                    enemies.Add(enemy);
                    enemy = enemies[0];
                }
            }

            if (enemy != null && offensivePlaceablesRef.currentTeam != enemy.currentTeam)
            {
                moveToTarget = true;
                readyToShoot = true;
                offensivePlaceablesRef.target = enemy;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (enemy != null && offensivePlaceablesRef.currentTeam != enemy.currentTeam)
        {
            enemies.Remove(enemy);
            moveToTarget = false;
            enemy = null;
            offensivePlaceablesRef.followTarget = false;
        }
    }
}