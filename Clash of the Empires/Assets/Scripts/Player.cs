using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ClashOfTheEmpires
{

    public int currentTeam = 1;
    public GameObject enemy;
    public int money = 1000;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            money += 100;
        }
    }

    void Movement()
    {

    }

    public void UnitPlacement()
    {

    }
}
