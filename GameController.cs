using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text Text;
    public int life;

    public GUIText scoreText;
    public int score;

    //adds the value of life to the text

    void start()
    {
        score = 0;
        updateScore();
        life = 50;
        Text.text = "Life " + life.ToString();
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }

        void update()
    {
            //press the plus key to add 1 life

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {

            life += 1;
            Text.text = "Life: " + life.ToString();
        }
        // press the minus to take away a life 

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {

            life -= 1;
            Text.text = "Life " + life.ToString();
        }

        // Once life reaches 0 death message will appear

        if (life <= 0)
        {
            print("You DIED");
        }
    }
    public void addScore(int newScoreValue)
    {
        score += newScoreValue;
        updateScore();
    }

}