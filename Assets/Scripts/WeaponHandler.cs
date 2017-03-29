﻿using UnityEngine;
using System.Collections;

public class WeaponHandler : MonoBehaviour {

	public GameObject gunLocation; //Where the gun's position is

	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject currentWeapon;
	private Weapon currentWeaponScript;
	public GameObject bulletPrefab;
	public Canvas canvas;
	private CanvasHandler canvasHandlerScript;
	public GameObject bulletSpawn;
	public float nextFire = 0.0f;

	public GameObject[] weaponPrefabs; //Array of all purchaseable weapons

	// Use this for initialization
	void Start () {

		weaponPrefabs = Resources.LoadAll<GameObject> ("WeaponPrefabs");
		canvasHandlerScript = (CanvasHandler)canvas.GetComponent (typeof(CanvasHandler));
		canvasHandlerScript.setWeaponObject (weapon1);
		weapon1.SetActive (true);
		currentWeapon = weapon1;
		currentWeaponScript = (Weapon)weapon1.GetComponent (typeof(Weapon));
		weapon2.SetActive (false);
	}

	void Update() {


		if (Input.GetKeyDown("1")) {
			Debug.Log ("key one");
			canvasHandlerScript.setWeaponObject (weapon1);
			weapon1.SetActive (true);
			currentWeapon = weapon1;
			weapon2.SetActive (false);
			currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));

		}

		else if (Input.GetKeyDown ("2")) {
			canvasHandlerScript.setWeaponObject (weapon2);
			weapon2.SetActive (true);
			weapon1.SetActive (false);
			currentWeapon = weapon2;
			currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));


		}


		//FIRING WEAPON
		if (Input.GetMouseButton(0) && Time.time > nextFire && currentWeaponScript.getClipAmount() > 0) {
			nextFire = Time.time + canvasHandlerScript.getWeaponScript ().getFireRate ();
			canvasHandlerScript.fireBullet ();
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 50;
			Destroy (bullet, 1.0f);

		}


		//RELOAD
		if (Input.GetKeyDown (KeyCode.R)  && Time.time >currentWeaponScript.getReloadTime()) {
			currentWeaponScript.reload ();
		}


	}

	//Replaces the current weapon of the player with the one that is bought
	public void replaceWeapon(string weaponName) {
		//Weapon in position 1
		if (currentWeapon.name.Equals(weapon1.name)) {
			Destroy (weapon1.gameObject);
			weapon1 = (GameObject)Instantiate (getWeapon (weaponName));
			weapon1.transform.position = Vector3.zero;
			canvasHandlerScript.setWeaponObject (weapon1);
			Destroy (currentWeapon.gameObject);
			currentWeapon = weapon1;
		
		//Weapon in position 2
		} else if (currentWeapon.name.Equals (weapon2.name)) {
			Destroy (weapon2.gameObject);
			weapon2 = (GameObject)Instantiate (getWeapon (weaponName));
			weapon2.transform.position = Vector3.zero;
			canvasHandlerScript.setWeaponObject (weapon2);
			Destroy (currentWeapon.gameObject);
			currentWeapon = weapon2;
		}

		currentWeapon.transform.SetParent (transform, false);
		currentWeapon.transform.position = gunLocation.transform.position;
		currentWeapon.transform.rotation = gunLocation.transform.rotation;
		currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));


	}


	public float getDamage() {
		return currentWeaponScript.getDamage ();
	}

	//Returns weapons of the prefabs of weapon objects
	public GameObject getWeapon(string weaponName) {
		GameObject weapon = null;
		for (int i = 0; i < weaponPrefabs.Length; i++) {
			if (weaponPrefabs [i].name.Equals (weaponName)) {
				weapon = weaponPrefabs [i];
				break;
			}
		}
		return weapon;
	}
}
