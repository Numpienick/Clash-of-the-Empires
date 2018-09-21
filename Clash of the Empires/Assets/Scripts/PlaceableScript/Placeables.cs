using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable", menuName = "Placeable/Placeable")]
public class Placeables : Player
{
    int cost;
    public float CurrentHealth;
    public float MaxHealth = 100;


    private Player playerRef;
    public GameObject Placeable;

    public float GetHealthPct()
    {
        return CurrentHealth / MaxHealth;
    }


    void Start()
    {
        CurrentHealth = MaxHealth;
        playerRef = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(6);

        if (Input.GetKeyDown(KeyCode.P))
            SummonUnit();
    }

    void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;



        Debug.Log("ouch");


        if (CurrentHealth <= 0 && gameObject.tag == "Placeables")
            Die();
    }

    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("dead");

        Destroy(gameObject);


    }

    public void SummonUnit()
    {
        Debug.Log("Unit summoned");
        int unit = Random.Range(0, 5);
        Debug.Log(unit);
        if (unit == 0)
            cost = 100;

        if (unit == 1)
            cost = 200;

        if (unit == 2)
            cost = 300;

        if (unit == 3)
            cost = 400;

        if (unit == 4)
            cost = 500;

        playerRef.money -= cost;
    }
}