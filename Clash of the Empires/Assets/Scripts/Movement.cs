﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {


    Camera cam;
    public LayerMask groundLayer;
    public NavMeshAgent playerAgent;

    public List<GameObject> currentlySelected;
    public Click checkSelected;
 

    #region Monobehaviour API


    void Awake ()
    {
        cam = Camera.main;
    }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            playerAgent.SetDestination(GetPointUnderCursor());     
        }
	}

    #endregion

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
