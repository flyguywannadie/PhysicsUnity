using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
	[SerializeField, Range(1, 10)] float speed = 2.0f;
	[SerializeField, Range(1, 10)] float jumpForce = 1.0f;
	[SerializeField, Range(1, 10)]	float pushpower = 2.0f;
	[SerializeField] Transform view;
	[SerializeField] Rig rig;

	private CharacterController controller;
	[SerializeField]private Vector3 velocity;
	private bool onGround;

	[SerializeField] private Animator anims;

	private void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
		rig.weight = (anims.GetBool("Equipped") ? 1 : 0);
	}

	void Update()
	{
		anims.SetFloat("VelocityY", velocity.y);

		onGround = controller.isGrounded;
		if (onGround && velocity.y < 0)
		{
			velocity.y = 0f;
		}

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));//
		move = Vector3.ClampMagnitude(move, 1);
		move = Quaternion.Euler(0, view.rotation.y, 0) * move;

		controller.Move(move * Time.deltaTime * speed);

		if (move != Vector3.zero)
		{
			//gameObject.transform.forward = move;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * speed);
		}

		// Changes the height position of the player..
		if (Input.GetButtonDown("Jump") && onGround)
		{
			velocity.y += Mathf.Sqrt(jumpForce * -3.0f * Physics.gravity.y);
		}

		velocity.y += Physics.gravity.y * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.E))
		{
			anims.SetBool("Equipped", !anims.GetBool("Equipped"));
			rig.weight = (anims.GetBool("Equipped") ? 1 : 0);
		}


		anims.SetBool("OnGround", onGround);
		anims.SetFloat("Speed", move.magnitude);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;

		// no rigidbody
		if (body == null || body.isKinematic)
		{
			return;
		}

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3)
		{
			return;
		}

		// Calculate push direction from move direction,
		// we only push objects to the sides never up and down
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

		// If you know how fast your character is trying to move,
		// then you can also multiply the push velocity by that.

		// Apply the push
		body.velocity = pushDir * pushpower;
	}
}
