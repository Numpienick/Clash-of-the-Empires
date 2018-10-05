using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Vector3 velocity = new Vector3(0, 5, 8);
    private float gravity = 9.8f;

    public float speed = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime * speed;

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
