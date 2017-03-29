using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

	GameObject player;

	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		WeaponHandler currentWeapon = (WeaponHandler)player.GetComponent (typeof(WeaponHandler));
		int gunDamage = (int)currentWeapon.getDamage ();

		if (col.gameObject.name.Equals ("Zombie(Clone)")) {
			GameObject zombie = col.gameObject;
			ZombieBehaviour zombieScript = (ZombieBehaviour)zombie.GetComponent (typeof(ZombieBehaviour));
			zombieScript.loseHealth (gunDamage);
			Debug.Log ("Zombie has: " + zombieScript.getHealth());
		}


	}
}
