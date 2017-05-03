using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDoor : MonoBehaviour {

	GameObject player;
	GameObject canvas;
	public float distance;
	public double buyRange = 3;
	Text buyText;
	public Text buyTextPrefab;
	public int scorePrice;

	GameObject scoreObject;
	Score playerScore;

	GameObject spawnerObject;
	Spawner spawnerScript;

	void Start () {
		player = GameObject.FindWithTag("Player");
		canvas = GameObject.Find ("Canvas");
		scoreObject = GameObject.Find ("Score");
		spawnerObject = GameObject.Find ("SpawnerManager");
		spawnerScript = (Spawner)spawnerObject.GetComponent (typeof(Spawner));
		playerScore = (Score)scoreObject.GetComponent (typeof(Score));
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, player.transform.position);

		if (distance < buyRange) {
			if (buyText == null) {
				buyText = (Text)Instantiate (buyTextPrefab);
				buyText.transform.SetParent (canvas.transform, false);
				buyText.text = "'T' to open for " + scorePrice.ToString();
			}
			if (Input.GetKey (KeyCode.T) && playerScore.getScore() >= scorePrice) {
				Destroy (transform.gameObject);
				playerScore.loseScore (scorePrice);
				Destroy (buyText.transform.gameObject);
				spawnerScript.removeDoor (transform.name);
			}
		} else {
			if (buyText != null) {
				Destroy (buyText.transform.gameObject);
			}
		}
	}
}
