using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour {
    GameObject player;
    public int moveSpeed = 1;
    public int Health = 100;
    NavMeshAgent agent;


    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player"); //Load player data into this object
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(player.transform.position);
        //transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
	}
}
