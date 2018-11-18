using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float expDmg = 150f;
    Placeables enemyVar;
    private float currentTeam;
    Placeables myUnit;
    public List<Placeables> enemyUnit = new List<Placeables>(0);

    // Use this for initialization.
    private void Awake()
    {
        StartCoroutine(Explode());
        myUnit = GetComponentInParent<OffensivePlaceables>();
        currentTeam = 1;
        transform.parent = null;
    }

    IEnumerator Explode()
    {
        //Creates an array of colliders in range when the explosion effect is instantiated.
        Collider[] colliders = Physics.OverlapSphere(transform.position, 75.0f);

        //Does damage to the right object
        foreach (Collider collider in colliders)
        {
            Placeables enemy = collider.GetComponentInParent<Placeables>();
            enemyUnit.Add(enemy);
        }
        for (int i = 0; i < enemyUnit.Count; i++)
        {
            Debug.Log(enemyUnit[i]);
            if (enemyUnit[i] != null)
            {
                if (enemyUnit[i].currentTeam != currentTeam)
                {
                    Debug.Log(enemyUnit[i].currentTeam);
                    enemyUnit[i].DealDamage(expDmg);
                    if (enemyUnit[i].currentHealth <= 0 || enemyUnit[i] == null)
                    {
                        Debug.Log("target is getting removed");
                        enemyUnit.Remove(enemyUnit[i]);
                    }
                }
            }
        }

        yield return new WaitForSeconds(3f);
        //Waits three seconds until the patricle effect has fully stopped, and then destroys itself.
        Destroy(gameObject);
    }
}
