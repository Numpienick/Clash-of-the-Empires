using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIOpponent : ClashOfTheEmpires
{
    public Player enemy;
    public int money = 1000;

    private MainGrid grid;

    public List<Placeables> friendlyUnits;
    private NavMeshAgent agent;

    public Vector3 finalPosition;

    public GameObject[] units;

    public List<Placeables> enemyUnits;

    bool mining = false;

    // Use this for initialization
    void Start()
    {
        grid = FindObjectOfType<MainGrid>();
        InvokeRepeating("GoingToAttack", 15, 10);
        Invoke("PlaceUnit", Random.Range(1, 4));
    }

    // Update is called once per frame
    void Update()
    {
        //need to remove dead units
        Placeables[] allUnits = FindObjectsOfType<Placeables>();
        foreach (Placeables unit in allUnits)
        {
            if (unit.currentTeam != currentTeam && !enemyUnits.Contains(unit))
            {
                enemyUnits.Add(unit);
            }
        }

        if (mining && money > 600)
            mining = false;
    }

    private void LateUpdate()
    {
        RemoveDeadUnits(friendlyUnits);
        RemoveDeadUnits(enemyUnits);
    }

    public void PlaceUnit()
    {
        float delay = Random.Range(1, 4);
        int cost = 0;
        int unit = Random.Range(0, 5);

        switch (unit)
        {
            //spawns turret
            case 0:
                cost = 500;
                break;
            //spawns goldmine
            case 1:
                cost = 500;
                break;
            //spawns archer
            case 2:
                cost = 175;
                break;
            //spawns barbarian
            case 3:
                cost = 125;
                break;
            //spawns bomber
            case 4:
                cost = 600;
                break;
            //spawns healer
            case 5:
                cost = 600;
                break;
        }

        if (!mining && money - cost <= 500 && money - cost >= 0)
        {
            SpawnBuilding(1, 0.025f);
            mining = true;
        }

        if (money - cost >= 500 && unit != 0 && unit != 1)
        {
            money -= cost;
            friendlyUnits.Add(Instantiate(units[unit], new Vector3(Random.Range(390, 490), 0, Random.Range(470, 370)), Quaternion.identity).GetComponent<Placeables>());
        }

        if (money - cost >= 500 && unit == 0)
        {
            money -= cost;
            SpawnBuilding(unit, 14.5f);
        }
        Invoke("PlaceUnit", delay);
    }

    public void SpawnBuilding(int unit, float yCount)
    {
        Vector3 clickPoint = Vector3.zero;
        if (currentTeam == 2)
            clickPoint = new Vector3(Random.Range(495, 5), yCount, Random.Range(5, 495));
        finalPosition = grid.GetNearestPointOnGrid(clickPoint, yCount);
        if (money - 200 >= 0 && grid.spawn)
        {
            units[unit].transform.position = finalPosition;
            money -= 200;
            Instantiate(units[unit], finalPosition, Quaternion.identity);
        }
    }
    void RemoveDeadUnits(List<Placeables> list)
    {
        for (int i = 0; i < list.Count;)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i);
            }
            else
                i++;
        }
    }

    void GoingToAttack()
    {
        int r = Random.Range(1, 2);
        if (r == 1 && friendlyUnits.Count > 0)
        {
            Placeables target = armyTarget();
            List<Placeables> army = CreateArmy();
            foreach (Placeables unit in army)
            {
                unit.target = target;
                unit.GetComponent<OffensivePlaceables>().FollowTarget(unit.target);
            }
        }
    }

    Placeables armyTarget()
    {
        return enemyUnits[Random.Range(0, enemyUnits.Count)];
    }

    List<Placeables> CreateArmy()
    {
        List<Placeables> army = new List<Placeables>();
        int armySize = Random.Range(0, friendlyUnits.Count);
        for (int i = 0; i <= armySize; i++)
        {
            int r = Random.Range(0, friendlyUnits.Count);
            army.Add(friendlyUnits[r]);
            //friendlyUnits.Remove(friendlyUnits[r]);
        }
        return army;
    }
}