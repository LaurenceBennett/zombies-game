using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public string name;
	public float damage;
	public int clipSize;
	public int clipAmount;
	public float firerate;
	public float reloadTime;




	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public int getClipSize() {
		return clipSize;
	}

	public int getClipAmount() {
		return clipAmount;
	}

	public float getReloadTime() {
		return reloadTime;
	}



	public float getFireRate() {
		return firerate;
	}

	public void fireGun() {
		this.clipAmount--;

	}

	public void reload() {
		this.clipAmount = clipSize;
	}

	public float getDamage() {
		return damage;
	}
}

