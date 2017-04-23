using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWeapon : MonoBehaviour {

	GameObject player;
	WeaponHandler weaponHandlerScript;
	GameObject canvas;
	public float distance;
	public double buyRange = 2;
	Text buyText;
	public Text buyTextPrefab;
	public int scorePrice;

	GameObject scoreObject;
	Score playerScore;


	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		weaponHandlerScript = (WeaponHandler)player.GetComponent (typeof(WeaponHandler));
		canvas = GameObject.Find ("Canvas");
		scoreObject = GameObject.Find ("Score");
		playerScore = (Score)scoreObject.GetComponent (typeof(Score));


	}

	private void Update()
	{
		distance = Vector3.Distance(transform.position, player.transform.position);

		if (distance < buyRange) {
			if (buyText == null) {
				buyText = (Text)Instantiate (buyTextPrefab);
				buyText.transform.SetParent (canvas.transform, false);
				buyText.text = "'G' to buy " + transform.name + " for " + scorePrice.ToString();
			}
			if (Input.GetKeyDown(KeyCode.G) && playerScore.getScore() >= scorePrice) {
				weaponHandlerScript.replaceWeapon (transform.name);
				playerScore.loseScore (scorePrice);
			}
		} else {
			if (buyText != null) {
				Destroy (buyText.transform.gameObject);
			}
		}
	}

}
