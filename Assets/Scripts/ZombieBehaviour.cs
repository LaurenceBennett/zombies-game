using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour {
    GameObject player;
    public int moveSpeed = 1;
    public int health = 100;
	NavMeshAgent agent;
    public float distance;
    public double attackRange = 2;
    public float nextAttack;
    public float attackSpeed = 1.0F;
    public int zombieDamage = 1;
    bool inRange;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player"); //Load player data into this object
        agent = GetComponent<NavMeshAgent>();

    }
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(player.transform.position);
        CheckDistance();
        if (inRange)
        {
            Attack();
        }
        else{
            print("Test");
        }
    }

	//Zombie loses health
	public void loseHealth(int value) {
		this.health -= value;
		if (this.health <= 0) {
			Debug.Log ("DEAD");
			Destroy (transform.gameObject);
		}
	}

	public int getHealth() {
		return health;
	}

    void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < attackRange)
        {
            inRange = true;
        }
    }
    void Attack()
    {
        var animator = gameObject.GetComponent<Animator>();
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            animator.SetTrigger("PlayerNear");
            PlayerStats playerController = (PlayerStats)player.GetComponent("PlayerStats");
            playerController.loseHealth(zombieDamage);
        }
        

    }
}
