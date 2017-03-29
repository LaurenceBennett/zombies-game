using UnityEngine;
using System.Collections;

public class Barricade : MonoBehaviour {

	private float timer = 0.0f;
	private bool mouseDown = false;
	public Material fixedWall;
	public bool fixedB = false;
	public GameObject player;

	// Use this for initialization
	void Start () {
		this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!fixedB && PlayerLook.barricadeUse) {
			if (mouseDown) {
				timer += 0.1f;
			} else {
				timer = 0.0f;
			}
			if (timer >= 6) {
				Debug.Log ("Fixed!");
				this.GetComponent<MeshRenderer> ().material = fixedWall;
			}
		}


	}

	void OnMouseDown() {
		Debug.Log ("Down");
		mouseDown = true;
	}

	void OnMouseUp() {
		Debug.Log ("Up");
		mouseDown = false;
	}
		
}
