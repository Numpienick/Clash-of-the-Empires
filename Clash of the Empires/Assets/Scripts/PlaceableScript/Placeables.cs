using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Placeables : MonoBehaviour
{
    [HideInInspector]
    public bool dead = false;

    public int currentTeam;
    public float currentHealth = 20f;
    public float maxHealth = 100f;

    public Vector3 spawnpoint;

    private Transform healthBar;
    [HideInInspector]
    public Canvas canvas;
    [HideInInspector]
    public Slider healthFill;

    [HideInInspector]
    public float healthbarOffsetY = 1.13f;

    public NavMeshAgent agent;
    public CheckForEnemy checkForEnemyRef;

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

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void DealDamage(float damageValue)
    {
        currentHealth -= damageValue;
    }

    public void Die()
    {
        dead = true;
        CheckForEnemy scriptRef = GetComponentInChildren<CheckForEnemy>();
        if (scriptRef != null && scriptRef.enemy != null)
            scriptRef.enemy.checkForEnemyRef.enemies.RemoveAt(0);

        if (gameObject != null)
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