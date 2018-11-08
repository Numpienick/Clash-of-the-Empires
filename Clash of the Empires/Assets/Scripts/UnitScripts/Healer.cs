using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : OffensivePlaceables
{
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.G))
            DealDamage(6);
    }
    void Heal(float healValue)
    {
        GetComponent<OffensivePlaceables>().currentHealth += healValue;
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
