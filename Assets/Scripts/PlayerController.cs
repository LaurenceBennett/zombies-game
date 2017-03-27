using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 3.0F;
	public float jumpSpeed = 8.0F;
	public float runSpeed = 10.0F;
	public float stamina = 30.0F;
	public float gravity = 20.0F;

	public static bool fixingPosition;
	
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		

		if (controller.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			if (Input.GetKey (KeyCode.LeftShift) && stamina > 0f) {
				moveDirection *= runSpeed;
				stamina -= Time.time * 0.1f;

			} else {
				if (stamina < 30f) {
					stamina += Time.time * 0.01f;
				}
				moveDirection *= walkSpeed;
			}
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

}
