using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Placeables : Player
{
    public float CurrentHealth = 20f;
    public float MaxHealth = 100f;
    public GameObject barracks;
    public GameObject goldmine;
    public GameObject archer;
    public GameObject barbarian;
    public GameObject achmed;
    public GameObject healer;
    public Player playerRef;

    private bool goldmineSelected = false;

    private Movement movementRef;

    public Vector3 spawnpoint;


    public float GetHealthPct()
    {
        return CurrentHealth / MaxHealth;
    }

    void Awake()
    {
        movementRef = Camera.main.GetComponent<Movement>();
        CurrentHealth = MaxHealth;
        playerRef = GetComponent<Player>();
    }

    void Update()
    {
        if (goldmineSelected == true && Input.GetMouseButtonDown(1))
        {
            PlaceUnit(200, 200, goldmine);
            Instantiate(goldmine, movementRef.GetPointUnderCursor(), Quaternion.identity);
            goldmineSelected = false;
        }

        if (goldmineSelected == true && Input.GetMouseButtonDown(0))
        {
            goldmineSelected = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(6);
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

    public void PlaceUnit(int cost, float unitHealth, GameObject unitType)
    {
        if (playerRef.money - cost >= 0)
        {
            playerRef.money -= cost;
        }
        if (playerRef.money - cost <= 0)
        {
            Debug.Log("Skere Tijden");
        }
    }

    public void Barracks()
    {
        PlaceUnit(100, 200, barracks);
        Debug.Log("Unit placed");
    }

    public void Goldmine()
    {
        goldmineSelected = true;
    }

    public void Archer()
    {
        PlaceUnit(300, 200, archer);
        Instantiate(archer, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
        // Debug.Log("Unit placed");
    }

    public void Barbarian()
    {
        PlaceUnit(400, 200, barbarian);
        Instantiate(barbarian, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
        // Debug.Log("Unit placed");
    }

    public void Achmed()
    {
        PlaceUnit(500, 200, achmed);
        Instantiate(achmed, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
        // Debug.Log("Unit placed");
    }

    public void Healer()
    {
        PlaceUnit(600, 200, healer);
        Instantiate(healer, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
        //  Debug.Log("Unit placed");
    }

}