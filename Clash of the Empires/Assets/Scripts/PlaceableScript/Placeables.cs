using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Placeables : Player
{
    public float currentHealth = 20f;
    public float maxHealth = 100f;
    public GameObject barracks;
    public GameObject goldmine;
    public GameObject archer;
    public GameObject barbarian;
    public GameObject achmed;
    public GameObject healer;
    public Player playerRef;

    private MainGrid grid;

    private bool goldmineSelected = false;

    private Movement movementRef;

    public Vector3 spawnpoint;

    private Transform healthBar;
    [HideInInspector]
    public Canvas canvas;
    [HideInInspector]
    public Slider healthFill;

    public float healthbarOffsetY = 1.13f;


    public float GetHealthPct()
    {
        return currentHealth / maxHealth;
    }

    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            healthBar = canvas.GetComponent<RectTransform>();
            healthFill = canvas.GetComponentInChildren<Slider>();
        }
        movementRef = Camera.main.GetComponent<Movement>();
        currentHealth = maxHealth;
        playerRef = GetComponent<Player>();
        grid = FindObjectOfType<MainGrid>();
    }

    void Update()
    {

        if (goldmineSelected == true && Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                goldMine(hitInfo.point);
            }
        }

        if (goldmineSelected == true && Input.GetMouseButtonDown(0))
        {
            goldmineSelected = false;
        }
    }

    public void Die()
    {
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

    public void goldMine(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        goldmine.transform.position = finalPosition;
        PlaceUnit(200, 200, goldmine);
        Instantiate(goldmine, finalPosition, Quaternion.identity);
        goldmineSelected = false;
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

    public void UpdateHealthBarPosition()
    {
        if (canvas != null)
        {
            Vector3 currentPos = transform.position;

            healthBar.position = new Vector3(currentPos.x, currentPos.y + healthbarOffsetY, currentPos.z);
            healthBar.LookAt(Camera.main.transform);
        }
    }

}