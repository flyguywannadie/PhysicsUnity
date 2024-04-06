using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAmmo : MonoBehaviour
{
	[SerializeField] LineRenderer lineRenderer;
	[SerializeField] LayerMask lazerLayerMask;
	[SerializeField] float radius;
	[SerializeField] float force;

	private void Start()
	{
		lineRenderer.SetPosition(0, transform.position + transform.right);
		lineRenderer.SetPosition(1, transform.forward * 1000);

		RaycastHit hit;
		Physics.Raycast(transform.position, transform.forward, out hit ,1000, lazerLayerMask);

		if (hit.transform)
		{
			var hits = Physics.OverlapSphere(hit.point, radius);

			if (hits.Length > 0)
			{
				foreach (var hit2 in hits)
				{
					if (hit2.GetComponentInParent<Rigidbody>())
					{
						hit2.GetComponentInParent<Rigidbody>().AddExplosionForce(force, hit.point, radius);
					}
					if (hit2.GetComponent<Rigidbody>())
					{
						hit2.GetComponent<Rigidbody>().AddExplosionForce(force, hit.point, radius);
					}
				}
			}
			ShooterManager.SHOOTERMANAGER.KillMovie();
			//print("Destroyed: " + hit.transform.name);
			Destroy(hit.transform.gameObject);
		}
	}
}
