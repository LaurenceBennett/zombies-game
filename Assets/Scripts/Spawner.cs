using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject zombie;
    public int spawnDelay;

	// Use this for initialization
	void Start () {
		zombie = (GameObject)Resources.Load("Zombie");
        spawnDelay = 5;
        StartCoroutine(SpawnTimer());
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator SpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(zombie, transform.position, transform.rotation);
        }
    }
}
