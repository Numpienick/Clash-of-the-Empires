using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Placeables : Player
{
    public float CurrentHealth;
    public float MaxHealth = 100;
    public GameObject barracks;
    public GameObject goldmine;
    public GameObject archer;
    public GameObject barbarian;
    public GameObject achmed;
    public GameObject healer;
    private Player playerRef;
    
    public float GetHealthPct()
    {
        return CurrentHealth / MaxHealth;
    }
    
    void Start()
    {
        CurrentHealth = MaxHealth;
        playerRef = GetComponent<Player>();
    }

    void Update()
    {
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
    }

    public void Barracks()
    {
        PlaceUnit(100, 200, barracks);
        Debug.Log("Unit placed");
    }

    public void Goldmine()
    {
        PlaceUnit(200, 200, goldmine);
        Debug.Log("Unit placed");
    }

    public void Archer()
    {
        PlaceUnit(300, 200, archer);
        Debug.Log("Unit placed");
    }

    public void Barbarian()
    {
        PlaceUnit(400, 200, barbarian);
        Debug.Log("Unit placed");
    }

    public void Achmed()
    {
        PlaceUnit(500, 200, achmed);
        Debug.Log("Unit placed");
    }

    public void Healer()
    {
        PlaceUnit(600, 200, healer);
        Debug.Log("Unit placed");
    }

}