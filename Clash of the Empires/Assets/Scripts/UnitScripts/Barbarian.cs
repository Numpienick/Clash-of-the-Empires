using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : OffensivePlaceables
{
    public Collider targetCollider;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    public void FollowTarget(GameObject target)
    {
        Debug.Log("following the target");
    }
}