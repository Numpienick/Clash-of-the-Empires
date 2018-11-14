using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achmed : OffensivePlaceables
{
    //List of variables 
    public AudioClip detonate;
    public GameObject explosion;
    
    public bool inDetonateRange = false;
    public bool isExploding = false;
    AudioSource source;

    public override void Start()
    {
        base.Start();
        source = GetComponent<AudioSource>();
        //Getting the component for the audio clips.
    }

    public override void Update()
    {
        base.Update();
        //Calls the target checking function.
        TargetCheck();
    }
    void TargetCheck()
    {

        //Checks if there is a target nearby, and if the isExploding bool is false. 
        if (target != null && isExploding == false)
        {

            float distance = Vector3.Distance(target.transform.position, this.transform.position);
            if (distance < 75)
                //Checks the distance between the target and Achmed, and if the target is in range, sets the inDetonateRange to true.
            {
                inDetonateRange = true;
            }
            if (target.GetComponent<OffensivePlaceables>().currentTeam != this.currentTeam && inDetonateRange == true)
                //Checks if the target's team is not equal to Achmed's current team, and if the target is in detonate range.
            {
                StartCoroutine(Explode());
                isExploding = true;
                //Calls the explode function and sets isExploding to true so the function doesn't get called a billion times.

            }
        }
    }

    IEnumerator Explode()
    {
        source.PlayOneShot(detonate);
        yield return new WaitForSeconds(1.5f);
        //Plays the sound clip, waits the time before the clip has played long enough, and instantiates 
        //a gameobject with a particle system, which has the full explosion sound effect, and which handles damage.
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        //Destroys achmed after detonation.

       

    }
}
