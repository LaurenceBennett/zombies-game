using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject exploision;
    public GameObject playerExplosion;
    public int scoreValue;
    public GameController gameController;

    // I wrote a variable to hold my reference to my instance of GameController
    //GameController is the class name, the second gameController is our variable
    void Start()
    {
        GameController gameControllerObject = GameController.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>;
        }
    }

    void onTriggerEnter (Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Instantiate(exploision, transform.position, transform.rotation);
        if (other.tag =="Player")
        { Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        // 
        gameController.addScore(scoreValue);
        Destroy( other.gameObject);
        Destroy(gameObject);
        }
    
}
