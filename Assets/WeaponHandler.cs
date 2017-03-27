using UnityEngine;
using System.Collections;

public class WeaponHandler : MonoBehaviour {

	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;
	public GameObject currentWeapon;
	private Weapon currentWeaponScript;
	public GameObject bulletPrefab;
	public Canvas canvas;
	private CanvasHandler canvasHandlerScript;
	public GameObject bulletSpawn;
	public float nextFire = 0.0f;
	// Use this for initialization
	void Start () {
		
		canvasHandlerScript = (CanvasHandler)canvas.GetComponent (typeof(CanvasHandler));
		canvasHandlerScript.setWeaponObject (weapon1);
		weapon1.SetActive (true);
		currentWeaponScript = (Weapon)weapon1.GetComponent (typeof(Weapon));

		weapon2.SetActive (false);
		weapon3.SetActive (false);
	}

	void Update() {


		if (Input.GetKeyDown("1")) {
			Debug.Log ("key one");
			canvasHandlerScript.setWeaponObject (weapon1);
			weapon1.SetActive (true);
			currentWeapon = weapon1;
			weapon2.SetActive (false);
			weapon3.SetActive (false);
			currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));

		}

		else if (Input.GetKeyDown ("2")) {
			canvasHandlerScript.setWeaponObject (weapon2);
			weapon2.SetActive (true);
			weapon1.SetActive (false);
			weapon3.SetActive (false);
			currentWeapon = weapon2;
			currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));


		}

		else if (Input.GetKeyDown ("3")) {
			canvasHandlerScript.setWeaponObject (weapon3);
			weapon2.SetActive (false);
			weapon1.SetActive (false);
			weapon3.SetActive (true);
			currentWeapon = weapon3;
			currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));


		}

		if (Input.GetMouseButton(0) && Time.time > nextFire && currentWeaponScript.getClipAmount() > 0) {
			nextFire = Time.time + canvasHandlerScript.getWeaponScript ().getFireRate ();
			canvasHandlerScript.fireBullet ();
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 50;
			Destroy (bullet, 1.0f);

		}

		if (Input.GetKeyDown (KeyCode.R)  && Time.time >currentWeaponScript.getReloadTime()) {
			currentWeaponScript.reload ();
		}


	}
}
