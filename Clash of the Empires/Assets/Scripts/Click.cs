﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    [SerializeField]
    private LayerMask clickablesLayer;

    public List<GameObject> selectedObjects;


    [HideInInspector]
    public List<GameObject> selectableObjects;

    public Vector3 mousePos1;
    public Vector3 mousePos2;

    public LayerMask groundLayer;
    Camera cam;

    private ClickOn checkSelected;


    private void Awake()
    {
        checkSelected = GameObject.FindObjectOfType<ClickOn>();
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
    }

    void Start()
    {
        selectedObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) & checkSelected.currentlySelected == true)
        {
            clearSelection();
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
        } 
    }

    void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>();

        if(Input.GetKey("left ctrl") == false)
        {
            clearSelection();
        }

        Rect selectionRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach (GameObject selectObject in selectableObjects)
        {
            if (selectObject != null)
            {
                if(selectionRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    selectedObjects.Add(selectObject);
                    selectObject.GetComponent<ClickOn>().currentlySelected = true;
                    selectObject.GetComponent<ClickOn>().ClickMe();
                    selectObject.GetComponent<Movement>().playerAgent.SetDestination(GetPointUnderCursor());
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
}