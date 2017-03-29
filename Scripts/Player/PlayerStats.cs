using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public int health;
	public int score;
	public Text healthText;

	void Start () {
		health = 100;
		score = 0;

		healthText.text = "Health: " + health.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loseHealth(int value) {
		this.health -= value;
		healthText.text = "Health: " + health.ToString();
	}
}
