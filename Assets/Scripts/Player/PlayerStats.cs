using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public int health;
	public Text healthText;
    Score playerScore;
    GameObject canvas;
    bool alive = true;

    private void Awake()
    {
        canvas = GameObject.FindWithTag("DeathScreen");
    }

    void Start () {
		health = 100;
		healthText.text = "Health: " + health.ToString();
        playerScore = (Score)canvas.GetComponent("DeathText");
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
			health = 0;
            alive = false;
            foreach (Transform child in canvas.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void loseHealth(int value)
    {
        this.health -= value;
        if (alive == true)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }

	public void fillHealth() {
		this.health = 100;
		healthText.text = "Health: " + health.ToString();
	}
}
