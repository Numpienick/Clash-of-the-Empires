using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barbarian : OffensivePlaceables
{
    private Units unitsRef;
    private void Start()
    {
        unitsRef = GetComponent<Units>();
    }

    private void Update()
    {

    }
    }