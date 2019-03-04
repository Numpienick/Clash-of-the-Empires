using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float damage = 200f;
    float scaledDamage;
    float maximumDamage = 250f;
    float minimumDamage = 75f;
    OffensivePlaceables target;
    Placeables enemyVar;
    public float currentTeam;
    public List<OffensivePlaceables> enemyUnits = new List<OffensivePlaceables>(0);
    public AudioClip boom;
    public AudioSource source;

    private float fractionalDistance;

    // Use this for initialization.
    private void Awake()
    {
        scaledDamage = maximumDamage - minimumDamage;
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        RaycastHit[] hitinfo = Physics.SphereCastAll(transform.position, 50, Vector3.forward, 50, int.MaxValue, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit hit1 in hitinfo)
        {
            //If it hits a body of a unit the script continues
            if (hit1.collider.tag == "BodyPart")
            {
                ///Calculates the damage depending on the distance between the explosion and the target
                fractionalDistance = (Mathf.Max(0, 75 - Vector3.Distance(transform.position, hit1.transform.position))) / 75;
                damage = scaledDamage * fractionalDistance + minimumDamage;
                target = hit1.transform.GetComponentInParent<OffensivePlaceables>();
                ///Also gets the target's script so it can deal damage

                //checks if the target is an enemy and if so deals damage
                if (target != null && target.currentTeam != currentTeam)
                {
                    enemyUnits.Add(target);
                    target = enemyUnits[0];
                    target.DealDamage(damage);
                    enemyUnits.RemoveAt(0);
                }
            }
        }
        //Waits three seconds until the patricle effect has fully stopped, and then destroys itself.
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
