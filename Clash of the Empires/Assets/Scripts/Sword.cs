using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    OffensivePlaceables offensivePlaceablesRef;
    OffensivePlaceables enemy = null;
    public bool attackReady = false;

    OffensivePlaceables[] targets = null;

    //Getting variable
    void Start()
    {
        offensivePlaceablesRef = GetComponentInParent<OffensivePlaceables>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "BodyPart")
            targets = other.GetComponentsInParent<OffensivePlaceables>();

        //Getting the enemy
        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].currentTeam != offensivePlaceablesRef.currentTeam)
                    enemy = targets[i];
            }

            //Checking whether or not to attack
            if (enemy != null)
                attackReady = true;

            if (enemy == null)
                attackReady = false;
        }
    }

    //Deal damage to the enemy
    public void Attack(float damage)
    {
        enemy.DealDamage(damage);
    }
}
