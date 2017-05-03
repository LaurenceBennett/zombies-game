using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour {
    // This whole behaviour just checks distance between the player and the zombie, if it's close enough the zombie attacks.
    GameObject player;
    public float distance;
    public double attackRange = 2;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        var animator = gameObject.GetComponent<Animator>();
        if (distance < attackRange)
        {
            animator.SetTrigger("PlayerNear");
			PlayerStats playerStatsScript = (PlayerStats)player.GetComponent (typeof(PlayerStats));
			playerStatsScript.loseHealth (15);
        }
    }

}
