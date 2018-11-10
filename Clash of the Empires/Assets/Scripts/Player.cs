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

    public GameObject enemy;
    public int money = 1000;

    public GameObject barracks;
    public GameObject goldmine;
    public GameObject archer;
    public GameObject barbarian;
    public GameObject achmed;
    public GameObject healer;

    private MainGrid grid;

    private bool goldmineSelected = false;

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
        currentTeam = 0;
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
    }

    void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>();

        if (Input.GetKey("left ctrl") == false)
        {
            clearSelection();
        }

        Rect selectionRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach (GameObject selectObject in selectableObjects)
        {
            if (selectObject != null)
            {
                if (selectionRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    selectedObjects.Add(selectObject);
                    selectObject.GetComponent<ClickOn>().currentlySelected = true;
                    selectObject.GetComponent<ClickOn>().ClickMe();
                }
            }
            else
            {
                remObjects.Add(selectObject);
            }
        }

        if (remObjects.Count > 0)
        {
            foreach (GameObject rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }

            remObjects.Clear();
        }
    }

    void clearSelection()
    {
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                if (obj != null)
                {
                    obj.GetComponent<ClickOn>().currentlySelected = false;
                    obj.GetComponent<ClickOn>().ClickMe();
                }
            }
            selectedObjects.Clear();
        }
    }

    public Vector3 GetPointUnderCursor()
    {
        Vector2 screenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(screenPosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitPosition;

        Physics.Raycast(ray, out hitPosition, 1000000, groundLayer);

        return hitPosition.point;
    }

    public void PlaceUnit(int cost, float unitHealth, GameObject unitType)
    {
        if (money - cost >= 0)
        {
            money -= cost;
            Instantiate(unitType, new Vector3(Random.Range(-390, -490), 0, Random.Range(-470, -370)), Quaternion.identity);
        }
        if (money - cost <= 0)
        {
            Debug.Log("Skere Tijden");
        }
    }

    public void Barracks()
    {
        PlaceUnit(100, 200, barracks);
        Debug.Log("Unit placed");
    }

    public void goldMine(Vector3 clickPoint)
    {
        finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        Debug.Log(finalPosition + "Clickpoint:" + clickPoint);
        if (money - 200 >= 0 && grid.spawn)
        {
            goldmine.transform.position = finalPosition;
            money -= 200;
            //Debug.Log(clickPoint + " finalpos: " + finalPosition);
            Instantiate(goldmine, finalPosition, Quaternion.identity);
            goldmineSelected = false;
        }
    }

    public void GoldMineSelected()
    {
        goldmineSelected = true;
    }

    public void Archer()
    {
        PlaceUnit(300, 200, archer);
    }

    public void Barbarian()
    {
        PlaceUnit(400, 200, barbarian);
    }

    public void Achmed()
    {
        PlaceUnit(500, 200, achmed);
    }

    public void Healer()
    {
        PlaceUnit(600, 200, healer);
    }
    public void SpawnGoldMine()
    {
        if (money - 200 >= 0)
        {
            goldmine.transform.position = finalPosition;
            money -= 200;
            //Debug.Log(clickPoint + " finalpos: " + finalPosition);
            Instantiate(goldmine, finalPosition, Quaternion.identity);
            goldmineSelected = false;
        }
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
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
