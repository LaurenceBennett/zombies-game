using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour {
    // This whole behaviour just checks distance between the player and the zombie, if it's close enough the zombie attacks.
    GameObject player;
    public float distance;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        var animator = gameObject.GetComponent<Animator>();
        if (distance < 2.5)
        {
            animator.SetTrigger("PlayerNear");
            //player.health -= 14 | ready to deduct health when player health is done
        }
    }

}
