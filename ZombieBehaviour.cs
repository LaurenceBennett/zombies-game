using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    GameObject player;
    public int health = 100;
    NavMeshAgent agent;
    public float distance;
    public double attackRange = 2;
    public float nextAttack;
    public float attackSpeed = 1.0F;
    public int zombieDamage = 1;
	bool inRange;

	public GameObject canvas;
	private CanvasHandler canvasHandlerScript;


    PlayerStats playerController;
    Animator animator;
    BoxCollider collision;
    AudioSource source;
    public int scoreValue = 50;
	public int headshotScoreValue = 75;
    Score playerScore;
    bool dead = false;
    GameObject scoreObject;
    GameObject spawnerObject;
    Spawner spawnerScript;
	public float hitDelay = 1F;
	GameObject zombieHead;



    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player"); //Load player data into this object
        playerController = (PlayerStats)player.GetComponent("PlayerStats");
        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        collision = gameObject.GetComponent<BoxCollider>();
        source = GetComponent<AudioSource>();
		canvas = GameObject.Find ("Canvas");
		canvasHandlerScript = (CanvasHandler)canvas.GetComponent (typeof(CanvasHandler));
        //canvas = GameObject.FindWithTag("ScoreText");

        scoreObject = GameObject.Find ("Score");
		playerScore = (Score)scoreObject.GetComponent (typeof(Score));

		spawnerObject = GameObject.Find ("SpawnerManager");
		spawnerScript = (Spawner)spawnerObject.GetComponent (typeof(Spawner));
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        CheckDistance();
        if (inRange && dead!=true)
        {
            Attack();
        }
        else
        {
        }
    }

	public void increaseHealth(int value) {
		this.health += value;	
	}

    //Zombie loses health
	public void loseHealth(int value, bool headshot)
    {
        
        this.health -= value;
        if (this.health <= 0) {
			
            dead = true;
            Debug.Log("DEAD");
            animator.SetTrigger("Dead");
            source.Play();
            agent.Stop();
			if (headshot) {
				playerScore.addScore (headshotScoreValue);
				canvasHandlerScript.headshot ();
			} else {
				playerScore.addScore (scoreValue);
			}
            StartCoroutine(DeathDelay(2.433F));
            collision.enabled = false;
            spawnerScript.killZombie();
        }
        else
        {
            StartCoroutine(HitDelay(hitDelay));
        }
    }

    public int getHealth()
    {
        return health;
    }



    bool CheckDistance()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < attackRange)
        {
            inRange = true;
            animator.SetTrigger("PlayerNear");
        }
        else
        {
            inRange = false;
        }
        return inRange;
    }
    void Attack()
    {
        var animator = gameObject.GetComponent<Animator>();
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            playerController.loseHealth(zombieDamage);
        }
    }

    IEnumerator DeathDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(transform.gameObject);
    }

    IEnumerator HitDelay(float time)
    {
        animator.SetTrigger("Shot");
        agent.Stop();
        yield return new WaitForSecondsRealtime(time);
		if (dead != true) {
			agent.Resume ();
		}
    }
}
