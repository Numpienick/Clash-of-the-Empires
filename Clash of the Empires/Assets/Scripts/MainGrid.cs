using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MainGrid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;
    [SerializeField]
    private LayerMask checkMask;

    [HideInInspector]
    public bool spawn;

    public Vector3 GetNearestPointOnGrid(Vector3 position, float yCount)
    {
        Vector3 pos;
        int xCount = Mathf.RoundToInt(position.x / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount,
            (float)zCount * size);

        int layerMask = checkMask;
        Collider[] intersecting = Physics.OverlapBox(pos = new Vector3(result.x, result.y + 3.75f, result.z), new Vector3(10, 7.5f, 10), Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore);
        if (intersecting.Length == 0)
        {
            spawn = true;
        }
        else
        {
            if (intersecting[0].transform.parent != null)
            {
                if (intersecting[0].transform.parent.position != Vector3.zero)
                {
                    RandomPoint(result, 20, out result);
                    result = GetNearestPointOnGrid(result, yCount);
                }
            }
            if (intersecting[0].transform.parent == null)
            {
                RandomPoint(result, 20, out result);
                result = GetNearestPointOnGrid(result, yCount);
            }
        }
        return result;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}