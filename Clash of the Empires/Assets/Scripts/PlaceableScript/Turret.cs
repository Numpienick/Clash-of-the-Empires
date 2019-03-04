using UnityEngine;

public class Turret : OffensivePlaceables
{
    public float shockRate = 0.6f;
    public GameObject lightningStrike;
    public GameObject endOfGO;
    public bool striking = false;

    public override void Start()
    {
        base.Start();
        lightningStrike.SetActive(false);
        healthbarOffsetY = 24;
        damage = 5;
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();

        striking = false;
    }

    public override void Update()
    {
        base.Update();
        moveToTarget = false;

        if (checkForEnemyRef.enemy != null)
            endOfGO.transform.position = checkForEnemyRef.enemy.transform.position;

        if (checkForEnemyRef.readyToShoot)
        {
            Electrocute();
        }

        if (!checkForEnemyRef.readyToShoot)
        {
            striking = false;
            lightningStrike.SetActive(false);
        }
    }

    void Electrocute()
    {
        if (striking == false)
        {
            striking = true;
            lightningStrike.SetActive(true);
            InvokeRepeating("Shock", 0.6f, shockRate);
        }
    }

    void Shock()
    {
        if (checkForEnemyRef.enemy != null)
        {
            target.DealDamage(damage);
        }
    }
}