using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour {

	public GameObject gunLocation; //Where the gun's position is
	public RectTransform sniperScope;
	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject currentWeapon;
	private Weapon currentWeaponScript;
	public GameObject bulletPrefab;
	public Canvas canvas;
	private CanvasHandler canvasHandlerScript;
	public GameObject bulletSpawn;
	public float nextFire = 0.0f;
	private float muzzleFlashTimerStart;
	public GameObject muzzleFlashObject;
	public float muzzleFlashTimer = 0.1f;
	public bool muzzleFlashEnabled = false;
	private bool zoomed = false;
	private Shader shader;
	public GameObject[] weaponPrefabs; //Array of all purchaseable weapons

	// Use this for initialization
	void Start () {

		sniperScope.gameObject.SetActive (false);
		weaponPrefabs = Resources.LoadAll<GameObject> ("WeaponPrefabs");
		canvasHandlerScript = (CanvasHandler)canvas.GetComponent (typeof(CanvasHandler));
		canvasHandlerScript.setWeaponObject (weapon1, false);
		canvasHandlerScript.setWeaponSlot1 (weapon1.name);
		canvasHandlerScript.setWeaponSlot2 (weapon2.name);
		weapon1.SetActive (true);
		currentWeapon = weapon1;
		currentWeaponScript = (Weapon)weapon1.GetComponent (typeof(Weapon));
		weapon2.SetActive (true);
		muzzleFlashTimerStart = muzzleFlashTimer;
	}

	void Update() {

		//Switch to weapon 1
		if (Input.GetKeyDown("1") && !zoomed) {
			currentWeaponScript.stopReload ();
			canvasHandlerScript.setWeaponObject (weapon1, false);
			weapon1.SetActive (true);
			weapon2.SetActive (false);
			currentWeapon = weapon1;
			currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));
		}

		//Switch to weapon 2
		else if (Input.GetKeyDown ("2") && !zoomed) {
			currentWeaponScript.stopReload ();
			canvasHandlerScript.setWeaponObject (weapon2, true);
			weapon2.SetActive (true);
			weapon1.SetActive (false);
			currentWeapon = weapon2;
			currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));


		}

		//Zoom in
		if (Input.GetMouseButtonDown (1) && currentWeaponScript.name.Equals ("Sniper rifle") && !zoomed) {
			Camera.main.fieldOfView = 20f;
			PlayerLook.lookSensitivity = 0.5F;
			sniperScope.gameObject.SetActive (true);
			zoomed = true;
		}

		//Zoom out
		else if(Input.GetMouseButtonDown (1) && currentWeaponScript.name.Equals ("Sniper rifle") && zoomed) {
			Camera.main.fieldOfView = 60f;
			PlayerLook.lookSensitivity = 3F;
			sniperScope.gameObject.SetActive (false);
			zoomed = false;


		}



		//FIRING WEAPON
		if (Input.GetMouseButton(0) && Time.time > nextFire && currentWeaponScript.getBulletAmount() > 0) {
			nextFire = Time.time + canvasHandlerScript.getWeaponScript ().getFireRate ();
			canvasHandlerScript.fireBullet ();
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 50;
			Destroy (bullet, 1.5f);
			muzzleFlashEnabled = true;
		}

		if (muzzleFlashEnabled == true){
			muzzleFlashObject.SetActive(true);
			muzzleFlashTimer -= Time.deltaTime;
		}
		if (muzzleFlashTimer <= 0){
			muzzleFlashObject.SetActive(false);
			muzzleFlashEnabled = false;
			muzzleFlashTimer = muzzleFlashTimerStart;
		}
		//RELOAD
		if (Input.GetKeyDown (KeyCode.R) && currentWeaponScript.getClipAmount() >= 1) {
			currentWeaponScript.reload ();
		}


	}

	//Replaces the current weapon of the player with the one that is bought
	public void replaceWeapon(string weaponName) {
		//Weapon in position 1
		if (currentWeapon.name.Equals(weapon1.name)) {
			Destroy (weapon1.gameObject);
			weapon1 = (GameObject)Instantiate (getWeapon (weaponName));
			weapon1.name = weaponName;
			weapon1.transform.position = Vector3.zero;
			canvasHandlerScript.setWeaponObject (weapon1, false);
			Destroy (currentWeapon.gameObject);
			currentWeapon = weapon1;
			canvasHandlerScript.setWeaponSlot1 (weapon1.name);


			//Weapon in position 2
		} else if (currentWeapon.name.Equals (weapon2.name)) {
			Destroy (weapon2.gameObject);
			weapon2 = (GameObject)Instantiate (getWeapon (weaponName));
			weapon2.name = weaponName;
			weapon2.transform.position = Vector3.zero;
			canvasHandlerScript.setWeaponObject (weapon2, true);
			Destroy (currentWeapon.gameObject);
			currentWeapon = weapon2;
			canvasHandlerScript.setWeaponSlot2 (weapon2.name);
		}

		currentWeapon.transform.SetParent (transform, false);
		currentWeapon.transform.position = gunLocation.transform.position;
		currentWeapon.transform.rotation = gunLocation.transform.rotation;
		currentWeaponScript = (Weapon) currentWeapon.GetComponent (typeof(Weapon));


	}


	public float getDamage() {
		return currentWeaponScript.getDamage ();
	}

	public float getRangeDamage() {
		return currentWeaponScript.getRangeDamage ();
	}

	public float getRange() {
		return currentWeaponScript.getRange ();
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
