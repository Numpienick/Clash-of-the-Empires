using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : OffensivePlaceables
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            DealDamage(6);
       

    }
    void DealDamage(float damageValue)
    {
        GetComponent<Units>().currentHealth += damageValue;
        Debug.Log("healing...");

    }
    void OnTriggerStay(Collider other)
    {
        Placeables unit = other.transform.root.GetComponent<Placeables>();
        if (currentHealth < 100)
        {

            Debug.Log("healing...");
        }
    }
}
