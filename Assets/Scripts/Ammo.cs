using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ammo : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float lifespane = 0;

	Rigidbody rb;

	void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (lifespane > 0)
        {
			Destroy(gameObject, lifespane);
		}

        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
    }

    void Update()
    {
        
    }
}
