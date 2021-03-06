﻿using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour {

	float yRotation;
	float xRotation;
	float lookSensitivity = 3;
	float currentXRotation;
	float currentYRotation;
	float yRotationV;
	float xRotationV;
	float lookSmoothnes = 0.1f; 
	public Camera camera;
	private bool mouseDown = false;
	public static bool barricadeUse = false;
	public float TargetDistance;
	private float allowedRange = 15;


	void start() {
	
		Cursor.lockState = CursorLockMode.Confined;
		camera = GetComponent<Camera>();


	}

	void Update ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
		xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
		xRotation = Mathf.Clamp(xRotation, -80, 100);
		currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
		currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothnes);
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);


		if (Input.GetButtonDown ("Fire1")) {
			RaycastHit shot;
			if(Physics.Raycast(transform.position, 
				transform.TransformDirection(Vector3.forward), out shot)) {
				TargetDistance = shot.distance;
				if (TargetDistance < allowedRange) {
					Debug.Log (shot.transform.name);
					shot.transform.SendMessage ("DeductPoints", 5, SendMessageOptions.DontRequireReceiver);
					barricadeUse = true;
				}
				if (TargetDistance > allowedRange) {
					barricadeUse = false;
				}
			}
		}

	}

	void OnMouseDown() {
		Debug.Log ("Down");
		mouseDown = true;
	}

	void OnMouseUp() {
		Debug.Log ("Up");
		mouseDown = false;
	}
}
