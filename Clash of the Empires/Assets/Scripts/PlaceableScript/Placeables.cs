using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Placeables : MonoBehaviour
{
    public bool turret = false;

    public bool moveToTarget;

    [HideInInspector]
    public Placeables target;

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
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
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