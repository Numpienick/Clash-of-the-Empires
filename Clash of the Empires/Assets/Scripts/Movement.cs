﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickablesLayer;

    private List<GameObject> selectedObjects;

    [HideInInspector]
    public List<GameObject> selectableObjects;
    public Vector3 mousePos1;
    public Vector3 mousePos2;
    private NavMeshAgent agent;
    public LayerMask groundLayer;
    public OffensivePlaceables offensivePlaceablesRef;

    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
    }

    void Update()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            float dist = agent.remainingDistance;

            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && dist - agent.stoppingDistance <= 0)
            {
                offensivePlaceablesRef.moveToTarget = true;
            }
            else
            {
                offensivePlaceablesRef.moveToTarget = false;
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
                offensivePlaceablesRef = unit.GetComponent<OffensivePlaceables>();
                offensivePlaceablesRef.moveToTarget = false;
                agent = unit.GetComponent<NavMeshAgent>();
                agent.speed = 50;
                agent.SetDestination(GetPointUnderCursor());
            }
        }

        //Shoots a ray and if it hits a unit makes the unit selected
        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickablesLayer, QueryTriggerInteraction.Collide))
            {
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();

                if (clickOnScript != null)
                {
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
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (mousePos1 != mousePos2)
            {
                SelectObjects();
            }
        }
    }

    //Creates the 2D rectangle and selects all the units inside of that rectangle
    void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>();

        if (Input.GetKey("left ctrl") == false)
        {
            clearSelection();
        }

        Rect selectionRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y); //The limegreen selectbox rectangle

        foreach (GameObject selectObject in selectableObjects)
        {
            if (selectObject != null && selectObject.GetComponent<Placeables>().currentTeam == player.currentTeam)
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

        //Removes the units that have been selected from the selectable units list
        if (remObjects.Count > 0)
        {
            foreach (GameObject rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }

            remObjects.Clear();
        }
    }

    //Returns the selected units to their non selected state
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitPosition;

        Physics.Raycast(ray, out hitPosition, 1000000, groundLayer);

        return hitPosition.point;
    }
}