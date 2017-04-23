using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasHandler : MonoBehaviour {

	public GameObject weaponObject;
	private Weapon weaponScript;
	public Text ammoText, headshotText;

	public Text weapon1, weapon2;

	void Start () {
		headshotText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		weaponScript = (Weapon)weaponObject.GetComponent (typeof(Weapon));
		ammoText.text = weaponScript.getBulletAmount ().ToString () + "/" + weaponScript.getClipAmount ().ToString ();
	
	
	}

	public void setWeaponObject(GameObject weapon, bool weaponNum) {
		this.weaponObject = weapon;
		if (weaponNum) {
			this.weapon2.color = Color.cyan;
			this.weapon1.color = Color.white;

		} else {
			this.weapon1.color = Color.cyan;
			this.weapon2.color = Color.white;
		}


	}

	public void fireBullet() {
		weaponScript.fireGun ();
	}

	public Weapon getWeaponScript() {
		return weaponScript;
	}

	public void setWeaponSlot1(string weaponName) {
		this.weapon1.text = weaponName;
	}

	public void setWeaponSlot2(string weaponName) {
		this.weapon2.text = weaponName;
	}

	public void headshot() {
		StartCoroutine(ShowMessage("Headshot!", 2));
	
	}

	IEnumerator ShowMessage(string message, float delay) {
		headshotText.text = message;
		headshotText.enabled = true;
		yield return new WaitForSeconds (delay);
		headshotText.enabled = false;
	}

}
