using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : OffensivePlaceables
{
    
    public override void Update()
    {
        base.Update();
        TargetCheck();
    }
    void Heal(float healValue)
    {
        GetComponent<Placeables>().currentHealth += healValue;
        Debug.Log("healing...");

    }
    void OnTriggerStay(Collider other)
    {
        Placeables unit = other.transform.root.GetComponent<Placeables>();
        if (currentHealth < 100)
        {
            Debug.Log("healing...");
        }
    }

    void TargetCheck()
    {

        //Checks if there is a target nearby, and if the isExploding bool is false. 
        if (target != null)
        {

        if ( target.GetComponent<Placeables>().currentHealth != target.GetComponent<Placeables>().maxHealth && target.GetComponent<Placeables>().currentTeam == this.currentTeam)
            {
            Debug.Log("Found a wounded ally!");

            }
        }
    }



}
