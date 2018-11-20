using UnityEngine;

public class Turret : OffensivePlaceables
{
    public float shockRate = 0.6f;
    public GameObject lightningStrike;
    public bool striking = false;

    public override void Start()
    {
        base.Start();
        healthbarOffsetY = 24;
        damage = 5;
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
        lightningStrike.SetActive(false);
        striking = false;
    }

    public override void Update()
    {
        base.Update();
        moveToTarget = false;

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
            if (checkForEnemyRef.enemy != null)
                lightningStrike.transform.LookAt(checkForEnemyRef.enemy.transform);
        }
    }
}