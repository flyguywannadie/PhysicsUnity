using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Aim : MonoBehaviour
{
	[SerializeField] float mouseSens;
	[SerializeField] float yawLimit = 50;
	[SerializeField] float pitchLimit = 50;

	Vector3 rotation = Vector3.zero;
	Vector2 prevAxis = Vector3.zero;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		prevAxis.x = -Input.GetAxis("Mouse Y");
		prevAxis.y = Input.GetAxis("Mouse X");
	}

	private void Update()
	{
		Vector3 axis = Vector3.zero;

		axis.x = -Input.GetAxis("Mouse Y") - prevAxis.x;
		axis.y = Input.GetAxis("Mouse X") - prevAxis.y;

		rotation.x += axis.x * mouseSens;
		rotation.y += axis.y * mouseSens;

		rotation.x = Mathf.Clamp(rotation.x, -pitchLimit, pitchLimit);
		rotation.y = Mathf.Clamp(rotation.y, -yawLimit, yawLimit);

		Quaternion qyaw = Quaternion.AngleAxis(rotation.y, Vector3.up);
		Quaternion qpitch = Quaternion.AngleAxis(rotation.x, Vector3.right);

		transform.localRotation = (qyaw * qpitch);
		
	}


	private void OnDrawGizmos()
	{
		Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100);
	}
}
