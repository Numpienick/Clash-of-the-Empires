﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGrid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    [HideInInspector]
    public bool spawn;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        int layerMask = 1 << 9;
       Collider[] intersecting = Physics.OverlapSphere(result, 0.01f, layerMask);
        if (intersecting.Length == 0)
        {
            Debug.Log("You can spawn");
            spawn = true;
        }
        else
        {
            Debug.Log("You can't spawn" + result + intersecting[0]);
            spawn = false;
        }

        return result;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 40; x += size)
        {
            for (float z = 0; z < 40; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }

        }
    }*/
}