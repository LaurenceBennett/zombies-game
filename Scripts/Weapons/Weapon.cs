using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour {

	public string name;
	public float damage, rangeDamage;
	public int bulletAmount; //Bullets 
	public int clipSize; //Amount of bullets in a single clip
	public int clipAmount; //Amount of clips
	public float range;
	bool timing = false;

	public GameObject canvas;
	public Transform loadingBar;
	public Transform loadingBarPrefab;

	public float currentAmount;
	public float speed;

	public float firerate;
	public float reloadTime;
    AudioSource source;


    private void Awake()
    {

    }
    // Use this for initialization
    void Start ()
	{
		canvas = GameObject.Find ("Canvas");	
        source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
	{
		
		source = GetComponent<AudioSource> ();
		if (timing) {
			if (currentAmount < 100) {
				currentAmount += (reloadTime * 6) * Time.deltaTime;

			}
			loadingBar.GetComponent<Image> ().fillAmount = currentAmount / 100;
		}
	}

	public int getBulletAmount() {
		return bulletAmount;
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

	public float getRange() {
		return range;
	}

	public float getFireRate() {
		return firerate;
	}

	public void fireGun() {
		this.bulletAmount--;
        source.Play();
	}

	public void reload() {
		if (!timing) {
			StartCoroutine (ReloadTimer (reloadTime));
		}

	}

	IEnumerator ReloadTimer(float time)
	{
	    timing = true;
		loadingBar = (Transform)Instantiate (loadingBarPrefab);
		loadingBar.gameObject.SetActive (true);
		loadingBar.transform.SetParent (canvas.transform, false);
		
		yield return new WaitForSecondsRealtime (time);
		Destroy (loadingBar.gameObject);
		currentAmount = 0;
		timing = false;
		this.bulletAmount = clipSize;
		if (transform.name.Equals ("Pistol")) {
			this.clipAmount -= 7;
		} else if (transform.name.Equals ("Machine gun")) {
			this.clipAmount -= 25;
		}
		else if (transform.name.Equals ("Shotgun")) {
			this.clipAmount -= 5;
		}
		else if (transform.name.Equals ("Assault rifle")) {
			this.clipAmount -= 30;
		}
		else if (transform.name.Equals ("Sniper rifle")) {
			this.clipAmount -= 5;
		}
	}

	public float getDamage() {
		return damage;
	}

	public float getRangeDamage() {
		return rangeDamage;
	}
}

