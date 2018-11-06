using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achmed : OffensivePlaceables
{

    public AudioClip Detonate;
    public GameObject explosion;
    public bool inDetonateRange = false;
    private CheckForEnemy checkForEnemyRef;
    public bool isExploding = false;
    AudioSource source;

    private void Start()
    {
        checkForEnemyRef = GetComponentInChildren<CheckForEnemy>();
        source = GetComponent<AudioSource> ();
    }

    void Update()
    {
        targetCheck();
    }
    void targetCheck()
    {
        GameObject target = checkForEnemyRef.mainTarget;
        if (target != null && isExploding == false)
        {
            float distance = Vector3.Distance(target.transform.position, this.transform.position);
            if (distance < 10)
            {
                
                inDetonateRange = true;
            }
            if (target.GetComponent<Units>().currentTeam != this.currentTeam && inDetonateRange == true)
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
