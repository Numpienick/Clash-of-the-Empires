using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    OffensivePlaceables offensivePlaceablesRef;
    [HideInInspector]
    public List<OffensivePlaceables> units = new List<OffensivePlaceables>(0);

    [HideInInspector]
    public OffensivePlaceables unit = null;

    OffensivePlaceables[] targets = null;

    private void Start()
    {
        offensivePlaceablesRef = GetComponentInParent<OffensivePlaceables>();
        StartCoroutine(Heal());
    }

    private void Update()
    {
        if (unit == null)
            offensivePlaceablesRef.moveToTarget = false;
        if (unit != null && units.Count > 1 && unit.currentHealth >= unit.maxHealth)
            units.Remove(unit);
    }

    IEnumerator Heal()
    {
        while (true)
        {
            yield return new WaitForSeconds(.7f);
            if (unit != null && offensivePlaceablesRef.currentTeam == unit.currentTeam && unit.currentHealth < unit.maxHealth)
                unit.currentHealth++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BodyPart")
            targets = other.GetComponentsInParent<OffensivePlaceables>();

        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].currentTeam == offensivePlaceablesRef.currentTeam)
                {
                    units.Add(targets[i]);
                    unit = units[0];
                }
            }
            offensivePlaceablesRef.moveToTarget = true;
            offensivePlaceablesRef.target = units[units.Count - 1];
        }
    }

    public void OnTriggerExit(Collider other)
    {
        OffensivePlaceables target = null;
        if (other.tag == "CheckForEnemy")
        {
            target = other.GetComponent<OffensivePlaceables>();
        }

        if (target != null && offensivePlaceablesRef.currentTeam == target.currentTeam)
        {
            units.Remove(target);
            offensivePlaceablesRef.moveToTarget = false;
            target = null;
        }
    }
}