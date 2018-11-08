using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achmed : OffensivePlaceables
{
    public AudioClip Detonate;
    public GameObject explosion;
    public bool inDetonateRange = false;
    public bool isExploding = false;
    AudioSource source;

    public override void Start()
    {
        base.Start();
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
        source = GetComponent<AudioSource>();
    }

    public override void Update()
    {
        base.Update();
        targetCheck();
    }
    void targetCheck()
    {
        GameObject target = checkForEnemyRef.mainTarget;
        if (target != null && isExploding == false)
        {
            float distance = Vector3.Distance(target.transform.position, this.transform.position);
            if (distance < 75)
            {

                inDetonateRange = true;
            }
            if (target.GetComponent<OffensivePlaceables>().currentTeam != this.currentTeam && inDetonateRange == true)
            {
                StartCoroutine(explode());
                isExploding = true;
            }
        }
    }

    IEnumerator explode()
    {
        source.PlayOneShot(Detonate);
        yield return new WaitForSeconds(1.5f);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }




}
