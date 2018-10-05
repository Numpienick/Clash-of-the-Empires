using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barbarian : OffensivePlaceables
{
    public Collider targetCollider;
    private Units unitsRef;

    private void Start()
    {
        unitsRef = GetComponent<Units>();
    }

    private void Update()
    {
        
    }
    public void FollowTarget(GameObject target)
    {
        Debug.Log("following the target");
        Vector3 targetPosition = target.transform.position;
        transform.LookAt(targetPosition);

        float distance = Vector3.Distance(target.transform.position, this.transform.position);

        if (distance > 5)
        {
            transform.Translate(Vector3.forward * unitsRef.agent.speed / 100);
        }
    }
}