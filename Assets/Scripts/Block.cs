using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    Vector3 originalPosition;

    [SerializeField] Material[] materials;
    [SerializeField] GameObject visuals;

    Rigidbody rb;
    bool destroyed = false;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        visuals.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
    }

	private void Update()
	{
		if (!destroyed)
        {
            if (transform.position.y < 0.25 || Vector3.Distance(originalPosition, transform.position) > 1)
            {
                if (ShooterManager.SHOOTERMANAGER)
                {
					destroyed = true;
					ShooterManager.SHOOTERMANAGER.KillMovie();
				}
            }
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (rb.velocity.magnitude > 2 || rb.angularVelocity.magnitude > 2)
        {
            audioSource.Play();
        }
	}
}
