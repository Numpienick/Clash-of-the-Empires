using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {


    Camera cam;
    public LayerMask groundLayer;
    public NavMeshAgent playerAgent;

    private ClickOn checkSelected;
 

    #region Monobehaviour API


    void Awake ()
    {
        cam = Camera.main;
        checkSelected = GameObject.FindObjectOfType<ClickOn>();
	}
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(1) & checkSelected.currentlySelected == true)
        {
            playerAgent.SetDestination(GetPointUnderCursor());     
        }
	}

    #endregion

    private Vector3 GetPointUnderCursor()
    {
        Vector2 screenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(screenPosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitPosition;

        Physics.Raycast(ray, out hitPosition, 1000000, groundLayer);

        return hitPosition.point;
    }
}
