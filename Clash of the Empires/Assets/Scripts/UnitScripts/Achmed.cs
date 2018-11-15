using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achmed : OffensivePlaceables
{
    public AudioClip detonate;
    public GameObject explosion;
    
    public bool inDetonateRange = false;
    public bool isExploding = false;
    AudioSource source;

    public override void Start()
    {
        base.Start();
        damage = 150;
        source = GetComponent<AudioSource>();
    }

    public override void Update()
    {
        base.Update();
        TargetCheck();
    }
    void TargetCheck()
    {
        if (target != null && isExploding == false)
        {
            float distance = Vector3.Distance(target.transform.position, this.transform.position);
            if (distance < 75)
            {
                inDetonateRange = true;
            }
            if (target.GetComponent<OffensivePlaceables>().currentTeam != currentTeam && inDetonateRange == true)
            {
                StartCoroutine(Explode());
                isExploding = true;
            }
        }
    }

    IEnumerator Explode()
    {
        source.PlayOneShot(detonate);
        yield return new WaitForSeconds(1.5f);
        GameObject boom = Instantiate(explosion, transform.position, transform.rotation, this.transform);       
        Destroy(gameObject);
    }
}
