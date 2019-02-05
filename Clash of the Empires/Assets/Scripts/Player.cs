using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : ClashOfTheEmpires
{
    [HideInInspector]
    public bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public Player enemy;
    public int money = 1000;

    public GameObject turret;
    public GameObject goldmine;
    public GameObject archer;
    public GameObject barbarian;
    public GameObject bomber;
    public GameObject healer;

    private MainGrid grid;

    private bool goldmineSelected = false;
    private bool turretSelected = false;

    [HideInInspector]
    public List<GameObject> selectableObjects;

    public List<GameObject> selectedObjects;

    [SerializeField]
    private LayerMask clickablesLayer;

    public Camera cam;

    public Vector3 mousePos1;
    public Vector3 mousePos2;
    private NavMeshAgent agent;
    public LayerMask groundLayer;

    public Vector3 finalPosition;


    // Use this for initialization
    void Start()
    {
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
        cam = Camera.main;
        grid = FindObjectOfType<MainGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pause Menu functionality
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {

                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown("m"))
        {
            money += 1000;
        }

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

        if (turretSelected == true && Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                Turret(hitInfo.point);
            }
        }

        if (turretSelected == true && Input.GetMouseButtonDown(0))
        {
            turretSelected = false;
        }
    }

    public Vector3 GetPointUnderCursor()
    {
        Vector2 screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitPosition;

        Physics.Raycast(ray, out hitPosition, 1000000, groundLayer);

        return hitPosition.point;
    }

    public void PlaceUnit(int cost, float unitHealth, GameObject unitType)
    {
        if (money - cost >= 500)
        {
            money -= cost;
            Instantiate(unitType, new Vector3(Random.Range(-390, -490), 0, Random.Range(-470, -370)), Quaternion.identity);
        }
    }

    public void Turret(Vector3 clickPoint)
    {
        finalPosition = grid.GetNearestPointOnGrid(clickPoint, 14.5f);
        if (money - 500 >= 0 && grid.spawn)
        {
            turret.transform.position = finalPosition;
            money -= 500;
            Instantiate(turret, finalPosition, Quaternion.identity);
            turretSelected = false;
        }
    }

    public void goldMine(Vector3 clickPoint)
    {
        finalPosition = grid.GetNearestPointOnGrid(clickPoint, 0.025f);
        if (money - 500 >= 0 && grid.spawn)
        {
            goldmine.transform.position = finalPosition;
            money -= 500;
            Instantiate(goldmine, finalPosition, Quaternion.identity);
            goldmineSelected = false;
        }
    }

    public void GoldMineSelected()
    {
        goldmineSelected = true;
    }

    public void TurretSelected()
    {
        turretSelected = true;
    }

    public void Archer()
    {
        PlaceUnit(175, 200, archer);
    }

    public void Barbarian()
    {
        PlaceUnit(125, 200, barbarian);
    }

    public void Bomber()
    {
        PlaceUnit(600, 200, bomber);
    }

    public void Healer()
    {
        PlaceUnit(600, 200, healer);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}