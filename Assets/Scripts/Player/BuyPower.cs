using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPower : MonoBehaviour {

	GameObject player;
	PlayerStats playerStatsScript;
	GameObject canvas;
	public float distance;
	public double buyRange = 3;
	Text buyText;
	public Text buyTextPrefab;
	public int scorePrice;

	GameObject scoreObject;
	Score playerScore;


	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		playerStatsScript = (PlayerStats)player.GetComponent (typeof(PlayerStats));
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
			if (Input.GetKeyDown (KeyCode.G) && playerScore.getScore() >= scorePrice) {
				playerScore.loseScore (scorePrice);
				if (transform.name.Equals ("Health")) {
					playerStatsScript.fillHealth ();
				}
			}
		} else {
			if (buyText != null) {
				Destroy (buyText.transform.gameObject);
			}
		}
	}

}
