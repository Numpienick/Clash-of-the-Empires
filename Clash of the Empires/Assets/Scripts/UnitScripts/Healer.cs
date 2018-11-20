using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : OffensivePlaceables {

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        agent.stoppingDistance = 11;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
