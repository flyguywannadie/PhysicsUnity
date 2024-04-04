using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	[System.Serializable]
	public struct Wheel
	{
		public WheelCollider collider;
		public Transform transform;
	}
	[System.Serializable]
	public struct Axle
	{
		public Wheel leftWheel;
		public Wheel rightWheel;
		public bool isMotor;
		public bool isSteering;
	}
	[SerializeField] Axle[] axles;
	[SerializeField] float maxMotorTorque;
	[SerializeField] float maxSteeringAngle;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			transform.position += Vector3.up;
			transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
		}
	}

	public void FixedUpdate()
	{
		float motor = Input.GetAxis("Vertical") * maxMotorTorque;
		float steering = Input.GetAxis("Horizontal") * maxSteeringAngle;
		foreach (Axle axle in axles)
		{
			if (axle.isSteering)
			{
				axle.leftWheel.collider.steerAngle = steering;
				axle.rightWheel.collider.steerAngle = steering;
			}
			if (axle.isMotor)
			{
				axle.leftWheel.collider.motorTorque = motor;
				axle.rightWheel.collider.motorTorque = motor;
			}
			UpdateWheelTransform(axle.leftWheel);
			UpdateWheelTransform(axle.rightWheel);
		}
	}

	public void UpdateWheelTransform(Wheel wheel)
	{
		wheel.collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
		wheel.transform.position = position;
		wheel.transform.rotation = rotation;
	}
}
