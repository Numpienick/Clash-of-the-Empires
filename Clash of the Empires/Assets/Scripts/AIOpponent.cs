using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIOpponent : MonoBehaviour
{
    public int gridSize = 20;
    public int currentTeam = 2;
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
        //Checks for all units that are in the map and adds the enemy units to a list
        Placeables[] allUnits = FindObjectsOfType<Placeables>();
        foreach (Placeables unit in allUnits)
        {
            if (unit.currentTeam != currentTeam && !enemyUnits.Contains(unit))
            {
                enemyUnits.Add(unit);
            }
        }

        //A check to prevent the AI from spawning too many goldmines
        if (mining && money > 600)
            mining = false;
    }

    private void LateUpdate()
    {
        //RemoveDeadUnits removes dead units from the lists
        RemoveDeadUnits(friendlyUnits);
        RemoveDeadUnits(enemyUnits);
    }

    public void PlaceUnit()
    {
        float delay = Random.Range(1, 4); //Random variable of the amount of time it takes for this function to be called again
        int cost = 0;
        int unit = Random.Range(0, 5); //Picks a random unit to spawn

        switch (unit)
        {
            //Spawns turret
            case 0:
                cost = 500;
                break;
            //Spawns goldmine
            case 1:
                cost = 500;
                break;
            //Spawns archer
            case 2:
                cost = 175;
                break;
            //Spawns barbarian
            case 3:
                cost = 125;
                break;
            //Spawns bomber
            case 4:
                cost = 600;
                break;
            //Spawns healer
            case 5:
                cost = 600;
                break;
        }

        //Spawning for the goldmine
        if (!mining && money - cost <= 500 && money - cost >= 0)
        {
            SpawnBuilding(1, 0.025f);
            mining = true;
        }

        //Spawning for any other unit that's not a goldmine or turret
        if (money - cost >= 500 && unit != 0 && unit != 1)
        {
            money -= cost;
            friendlyUnits.Add(Instantiate(units[unit], new Vector3(Random.Range(390, 490), 0, Random.Range(470, 370)), Quaternion.identity).GetComponent<Placeables>());
        }

        //Spawning for the turret
        if (money - cost >= 500 && unit == 0)
        {
            money -= cost;
            SpawnBuilding(unit, 14.5f);
        }
        Invoke("PlaceUnit", delay);
    }

    //Spawning of the goldmine and turret
    public void SpawnBuilding(int unit, float yCount)
    {
        Vector3 clickPoint = Vector3.zero;
        if (currentTeam == 2)
            clickPoint = new Vector3(Random.Range(495, 5), yCount, Random.Range(5, 495));
        finalPosition = grid.GetNearestPointOnGrid(clickPoint, yCount); //Getting the position for the building to spawn at

        //Instantiating of the building
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

    //This function gets called every 15 seconds and has a 50% chance (determined by the r int) of creating an army that will attack one of the targets
    void GoingToAttack()
    {
        int r = Random.Range(1, 2);

        if (r == 1 && friendlyUnits.Count > 0)
        {
            Placeables target = armyTarget(); //Gets a target for the army to attack
            List<Placeables> army = CreateArmy(); //Creates an army out of the friendly units it has available

            //Gives each unit in the army the target
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
        int armySize = Random.Range(0, friendlyUnits.Count); //Creates a random size of the army of atleast 1
        for (int i = 0; i <= armySize; i++)
        {
            //Picks out a random unit and puts it in the army
            int r = Random.Range(0, friendlyUnits.Count);
            army.Add(friendlyUnits[r]);
        }
        return army;
    }
}