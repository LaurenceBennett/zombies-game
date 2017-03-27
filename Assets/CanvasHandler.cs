using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasHandler : MonoBehaviour {

	public GameObject weaponObject;
	private Weapon weaponScript;
	public Text ammoText;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		weaponScript = (Weapon)weaponObject.GetComponent (typeof(Weapon));
		ammoText.text = weaponScript.getClipAmount ().ToString() + "/" + weaponScript.getClipSize ().ToString();
	
	
	}

	public void setWeaponObject(GameObject weapon) {
		this.weaponObject = weapon;
	}

	public void fireBullet() {
		weaponScript.fireGun ();
	}

	public Weapon getWeaponScript() {
		return weaponScript;
	}
}
