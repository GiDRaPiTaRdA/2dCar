using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisCarScript : MonoBehaviour {

	public KeyCode moveUp;
	public KeyCode moveDown;
	public int velocity;

	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (moveUp)) {
			rigidBody.velocity = new Vector2 (0f, velocity);
		} else if (Input.GetKey (moveDown)) {
			rigidBody.velocity = new Vector2 (0f, -velocity);
		} else {
			rigidBody.velocity = new Vector2 (0f, 0f);
		}
	}
}
