using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
	[SerializeField] float radius;
	[SerializeField] float force;

	private void OnCollisionEnter(Collision collision)
	{
		var hits = Physics.OverlapSphere(transform.position, radius);

        if (hits.Length > 0)
        {
			foreach (var hit in hits)
			{
				if (hit.GetComponentInParent<Rigidbody>())
				{
					hit.GetComponentInParent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
				}
				if (hit.GetComponent<Rigidbody>())
				{
					hit.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
				}
			}
		}

		Destroy(this.gameObject);
	}
}
