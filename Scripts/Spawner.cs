using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public int round = 1;
    public int zombieMaxCount = 10;
    public int zombieCount = 0; //Total zombies left
    public int zombiesKilled = 0;//Zombies killd by player
    public GameObject zombie;
    public float spawnDelay = 0.5f;
    public Text roundText;
    public Text killedZombies;
	string textToGo;
	public GameObject[] spawnRoom1;
	public GameObject[] spawnRoom2;
	public GameObject[] spawnRoom3;

	public GameObject[] doorCount;
	public List<GameObject> doors;

    // Use this for initialization
    void Start()
    {
		doorCount = GameObject.FindGameObjectsWithTag ("Door");
		for (int i = 0; i < doorCount.Length; i++) {
			doors.Add(doorCount[i]);
		}
		spawnRoom1 = GameObject.FindGameObjectsWithTag ("SpawnRoom1");
		spawnRoom2 = GameObject.FindGameObjectsWithTag ("SpawnRoom2");
		spawnRoom3 = GameObject.FindGameObjectsWithTag ("SpawnRoom3");

        roundText.text = "Round " + round.ToString();
        killedZombies.text =zombiesKilled + "/" + zombieMaxCount;
        zombie = (GameObject)Resources.Load("Zombie");
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
		updateText ();
        if (zombiesKilled >= zombieMaxCount)
        {
            newRound();
        }
    }

    IEnumerator SpawnTimer()
    {
        while (true && zombieCount != zombieMaxCount)
        {
            yield return new WaitForSeconds(spawnDelay);


			//Spawn Room 1
			if (getDoor ("Door1") && getDoor("Door2") && getDoor("Door3")) {
				GameObject spawnPosition = spawnRoom1 [Random.Range (0, spawnRoom1.Length)];
				Instantiate (zombie, spawnPosition.transform.position, transform.rotation);

			//Spawn Room 2
			} if (!getDoor ("Door1") && getDoor("Door4")) {
				int chooseRoom = Random.Range (0, 2);
				if (chooseRoom == 1) {
					GameObject spawnPosition = spawnRoom2 [Random.Range (0, spawnRoom2.Length)];
					Instantiate (zombie, spawnPosition.transform.position, transform.rotation);
				} else if (chooseRoom == 0) {
					GameObject spawnPosition = spawnRoom1 [Random.Range (0, spawnRoom1.Length)];
					Instantiate (zombie, spawnPosition.transform.position, transform.rotation);
				}
			}

			//Spawn Room 3
			else if (!getDoor("Door1") && !getDoor ("Door4")) {
				int chooseRoom = Random.Range (0, 3);
				if (chooseRoom == 2) {
					GameObject spawnPosition = spawnRoom3 [Random.Range (0, spawnRoom3.Length)];
					Instantiate (zombie, spawnPosition.transform.position, transform.rotation);
				}
				else if (chooseRoom == 1) {
					GameObject spawnPosition = spawnRoom2 [Random.Range (0, spawnRoom2.Length)];
					Instantiate (zombie, spawnPosition.transform.position, transform.rotation);
				} else if (chooseRoom == 0) {
					GameObject spawnPosition = spawnRoom1 [Random.Range (0, spawnRoom1.Length)];
					Instantiate (zombie, spawnPosition.transform.position, transform.rotation);
				}

			}
            zombieCount++;
        }
    }

    public void killZombie()
    {
        zombiesKilled++;
    }

    public void newRound()
    {
        zombieMaxCount += 2;
		if (zombieMaxCount > 20) {
			zombieMaxCount = 20;
		}
        zombiesKilled = 0;
        zombieCount = 0;
        round++;
        roundText.text = "Round " + round.ToString();
        StartCoroutine(SpawnTimer());

    }

	public void removeDoor(string name) {
		for (int i = 0; i < doors.Count; i++) {
			if (doors [i].name.Equals (name)) {
				doors.RemoveAt (i);
				break;
			}
		}

	}

	public bool getDoor(string name) {
		bool closed = false;
		for (int i = 0; i < doors.Count; i++) {
			if (doors [i].name.Equals (name)) {
				closed = true;
				break;
			}
		}
		return closed;
	}


	void updateText(){
		killedZombies.text = zombiesKilled+"/"+zombieMaxCount;
	}
}