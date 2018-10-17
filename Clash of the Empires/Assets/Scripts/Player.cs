using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : ClashOfTheEmpires
{
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
    private Units unitsRef;


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
        /*if (agent != null)
        {
            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance - agent.stoppingDistance <= 0)
            {
                unitsRef.followTarget = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject selectObjects in selectableObjects)
            {
                clearSelection();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (GameObject unit in selectedObjects)
            {
                unitsRef = unit.GetComponent<Units>();
                unitsRef.followTarget = false;
                agent = unit.GetComponent<NavMeshAgent>();
                agent.speed = 50;
                agent.SetDestination(GetPointUnderCursor());
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);


            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickablesLayer))
            {
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();

                if (Input.GetKey("left ctrl"))
                {
                    if (clickOnScript.currentlySelected == false)
                    {
                        selectedObjects.Add(rayHit.collider.gameObject);
                        clickOnScript.currentlySelected = true;
                        clickOnScript.ClickMe();
                    }
                    else
                    {
                        selectedObjects.Remove(rayHit.collider.gameObject);
                        clickOnScript.currentlySelected = false;
                        clickOnScript.ClickMe();
                    }
                }
                else
                {
                    clearSelection();

                    selectedObjects.Add(rayHit.collider.gameObject);
                    clickOnScript.currentlySelected = true;
                    clickOnScript.ClickMe();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (mousePos1 != mousePos2)
            {
                SelectObjects();
            }
        }*/

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
            Instantiate(unitType, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
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
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        goldmine.transform.position = finalPosition;
        PlaceUnit(200, 200, goldmine);
        Debug.Log(clickPoint +" finalpos: " + finalPosition);
        Instantiate(goldmine, finalPosition, Quaternion.identity);
        goldmineSelected = false;
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
}
