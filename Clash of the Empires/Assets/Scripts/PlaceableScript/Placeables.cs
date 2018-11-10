using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Placeables : MonoBehaviour
{
    public int currentTeam;
    public float currentHealth = 20f;
    public float maxHealth = 100f;
    public float unitHealth;

    public Vector3 spawnpoint;

    private Transform healthBar;
    [HideInInspector]
    public Canvas canvas;
    [HideInInspector]
    public Slider healthFill;

    private float healthbarOffsetY = 1.13f;

    public NavMeshAgent agent;
    public CheckForEnemy checkForEnemyRef;

    public float GetHealthPct()
    {
        return currentHealth / maxHealth;
    }

    public virtual void Awake()
    {
        healthbarOffsetY = 6;
        currentHealth = maxHealth;
    }

    public virtual void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            healthBar = canvas.GetComponent<RectTransform>();
            healthFill = canvas.GetComponentInChildren<Slider>();
        }        
    }

    public virtual void Update()
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