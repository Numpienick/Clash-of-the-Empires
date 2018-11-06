using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Vector3 velocity;
    private float gravity = 9.8f;

    public GameObject bulletEmitter;

    private void Start()
    {
        velocity = new Vector3(0, 1, 20);
    }
    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime * gravity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger != true)
        {
            Debug.Log(other.name);
            gravity = 0;
            velocity = new Vector3(0, 0, 0);

            this.transform.parent = other.transform;
        }
    }
}
