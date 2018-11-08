using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Placeables : ClashOfTheEmpires
{
    public float currentHealth = 20f;
    public float maxHealth = 100f;
    public float unitHealth;

    public Vector3 spawnpoint;

    private Transform healthBar;
    //[HideInInspector]
    public Canvas canvas;
    // [HideInInspector]
    public Slider healthFill;

    private float healthbarOffsetY = 1.13f;

    public float GetHealthPct()
    {
        return currentHealth / maxHealth;
    }

    void Awake()
    {
        healthbarOffsetY = 6;
        canvas = GetComponentInChildren<Canvas>();
        Debug.Log(canvas);
        if (canvas != null)
        {
            healthBar = canvas.GetComponent<RectTransform>();
            healthFill = canvas.GetComponentInChildren<Slider>();
        }
        currentHealth = maxHealth;
    }

    private void LateUpdate()
    {
        if (canvas != null)
        {
            UpdateHealthBarPosition();
            healthFill.value = currentHealth / maxHealth;
        }

        if (currentHealth > unitHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        Debug.Log("dead");
        Destroy(gameObject);
    }

    private void UpdateHealthBarPosition()
    {
        if (canvas != null)
        {
            Vector3 currentPos = transform.position;

            healthBar.position = new Vector3(currentPos.x, currentPos.y + healthbarOffsetY, currentPos.z);
            healthBar.LookAt(Camera.main.transform);
        }
    }
}