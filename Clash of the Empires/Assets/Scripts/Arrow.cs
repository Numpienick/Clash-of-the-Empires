using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 velocity;
    private float gravity = 9.8f;

    private OffensivePlaceables myUnit;
    public GameObject bulletEmitter;
    private float speed = 8f;

    private void Start()
        //Refrence to the unit and the arrow's starting velocity
    {
        myUnit = GetComponentInParent<OffensivePlaceables>();
        velocity = new Vector3(0, 1, 20);
    }
    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the hit collider is a trigger
       if (other.isTrigger != true)
        {
           // Sets the arrow's parent to that of the hit object, so that it sticks.
            gravity = 0;
            velocity = new Vector3(0, 0, 0);

           this.transform.parent = other.transform;
        }
        
       //Checks if the arrow has hit a bodypart and deals damage to the body part's parent.
        Placeables enemyUnit = null;
        if (other.CompareTag("BodyPart"))
        {
            enemyUnit = other.GetComponentInParent<Placeables>();
        }

        if (enemyUnit != null && enemyUnit.currentTeam != myUnit.currentTeam)
        {
            enemyUnit.DealDamage(myUnit.damage);
        }
    }
}
