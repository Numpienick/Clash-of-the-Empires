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
        // this shit broken atm
       if (other.isTrigger != true)
        {
           // Debug.Log("stuck "+other.name);
            gravity = 0;
            velocity = new Vector3(0, 0, 0);

           this.transform.parent = other.transform;
        }
        

        Placeables enemyUnit = null;
        if (other.CompareTag("BodyPart"))
        {
            enemyUnit = other.GetComponentInParent<Placeables>();
        }

        if (enemyUnit != null && enemyUnit.currentTeam != myUnit.currentTeam)
        {
            enemyUnit.DealDamage(myUnit.damage);
            enemyUnit = null;
        }
    }
}
