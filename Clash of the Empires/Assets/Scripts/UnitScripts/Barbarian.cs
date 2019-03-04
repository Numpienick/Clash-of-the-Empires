using UnityEngine;

public class Barbarian : OffensivePlaceables
{
    Sword swordScript;

    public override void Start()
    {
        base.Start();
        fireRate = 0.9f;
        damage = 20;
        agent.stoppingDistance = 6;
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
        swordScript = GetComponentInChildren<Sword>();
    }

    public override void Update()
    {
        base.Update();
        //Attacking according to the fireRate
        if (checkForEnemyRef.readyToShoot == true && moveToTarget == true && Time.time >= nextTimeToFire && swordScript.attackReady == true)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Attack();
        }
    }

    void Attack()
    {
        //Call the function that'll deal damage
        swordScript.Attack(damage);
    }
}