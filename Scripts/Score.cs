using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
	public static int score = 10000;

    void awake()
    {
        score = 10000;
        updateScore();
        scoreText = GetComponent<Text>();
    }


    void Update()
    {
        updateScore();
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void addScore(int newScoreValue)
    {
        score += newScoreValue;
		Debug.Log (score);
        updateScore();
    }

    public void loseScore(int newScoreValue)
    {
        score -= newScoreValue;
        updateScore();
    }

    public int getScore() {
		return score;
	}

}