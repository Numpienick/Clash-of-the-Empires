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
    void OnTriggerEnter(Collider other)
    {
        if (currentHealth < 100)
        {
            
        }
    }
}
