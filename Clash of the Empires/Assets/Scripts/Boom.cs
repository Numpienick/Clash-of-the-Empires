using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float expDmg = 200f;
    OffensivePlaceables target;
    Placeables enemyVar;
    private float currentTeam;
    Placeables myUnit;
    public List<OffensivePlaceables> enemyUnits = new List<OffensivePlaceables>(0);
    public AudioClip boom;
    private AudioSource source;

    // Use this for initialization.
    private void Awake()
    {
        StartCoroutine(Explode());
        // myUnit = GetComponentInParent<OffensivePlaceables>();
        //transform.parent = null;
        source = GetComponent<AudioSource>();
    }

    IEnumerator Explode()
    {
        //Creates an array of colliders in range when the explosion effect is instantiated.
        Collider[] colliders = Physics.OverlapSphere(transform.position, 75.0f);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "BodyPart")
            {
                target = collider.GetComponentInParent<OffensivePlaceables>();
                enemyUnits.Add(target);
                target = enemyUnits[0];
            }
        }

        if (target != null)
        {
            target.DealDamage(expDmg);            
            enemyUnits.RemoveAt(0);
        }
        source.PlayOneShot(boom);
        //Waits three seconds until the patricle effect has fully stopped, and then destroys itself.
        yield return new WaitForSeconds(1.5f);
        Destroy(this);
    }
}
