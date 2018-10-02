using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : OffensivePlaceables
{
    private SphereCollider detectingRange;
    void Start()
    {
        detectingRange = GetComponent<SphereCollider>();
        healthbarOffsetY = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(6);

        UpdateHealthBarPosition();
        healthFill.value = currentHealth / maxHealth;
    }

    void DealDamage(float damageValue)
    {
        currentHealth -= damageValue;
        Debug.Log("ouch");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
