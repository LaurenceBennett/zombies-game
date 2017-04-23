using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHit : MonoBehaviour {

	GameObject player;
	public float distance;


	void Start () {
		player = GameObject.FindWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		WeaponHandler currentWeapon = (WeaponHandler)player.GetComponent (typeof(WeaponHandler));
		int gunDamage = (int)currentWeapon.getDamage ();
		int gunDamageRange = (int)currentWeapon.getDamageRange ();

		if (col.gameObject.name.Equals ("Zombie(Clone)")) {
			GameObject zombie = col.gameObject;
            ZombieBehaviour zombieScript = (ZombieBehaviour)zombie.GetComponent (typeof(ZombieBehaviour));
			if (CheckDistance ()) {
				zombieScript.loseHealth (gunDamage, false);
				Debug.Log ("In range");
			} else {
				zombieScript.loseHealth (gunDamageRange, false);
				Debug.Log ("Out of range");
			}

			Debug.Log ("Zombie has: " + zombieScript.getHealth());

			Destroy (transform.gameObject);
		}

		else if (col.gameObject.name.Equals("mesh_Head")) {
			GameObject zombie = col.transform.parent.gameObject;
			ZombieBehaviour zombieScript = (ZombieBehaviour)zombie.GetComponent (typeof(ZombieBehaviour));
			double DoubDamage = (double) gunDamage * 1.5; //Headshot damage
			int damage = (int) DoubDamage;
			if (zombieScript.getHealth () > 0) {
				zombieScript.loseHealth (damage, true);
			}
			Destroy (transform.gameObject);
			Debug.Log ("HEADSHOT");

		}


	}


	bool CheckDistance()
	{
		bool inRange = false;
		WeaponHandler currentWeapon = (WeaponHandler)player.GetComponent (typeof(WeaponHandler));
		distance = Vector3.Distance(transform.position, player.transform.position);
		if (distance < currentWeapon.getRange())
		{
			inRange = true;
			Debug.Log ("In range!");
		}
		else
		{
			inRange = false;
		}
		return inRange;
	}
}
