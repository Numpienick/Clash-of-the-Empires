using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {

    public float expDmg = 150f;
    Placeables enemyVar;
    // Use this for initialization.
    private void Awake()
    {
        StartCoroutine(Explode());
    }


    IEnumerator Explode()
    {
        //Creates an array of colliders in range when the explosion effect is instantiated.
        Collider[] colliders = Physics.OverlapSphere(transform.position, 75.0f);
        

        foreach (Collider collider in colliders)
        {
        // Checks if the collider has the Placeable script.   
            if (enemyVar != null)
            {
                enemyVar.GetComponentInParent<Placeables>().DealDamage(expDmg); 
                // Deals damage according to the publicly set explosion damage variable.
            }

        }

        yield return new WaitForSeconds(3f);
        //Waits three seconds until the patricle effect has fully stopped, and then destroys itself.
        Destroy(gameObject);
    }
}
