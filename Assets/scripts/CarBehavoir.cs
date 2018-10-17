using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavoir : MonoBehaviour {

	Vehicle vehicle;

	public Camera cam;

	public double Speed { get{return vehicle.RelativeSpeed;}}
	public double TurnAngle { get{return vehicle.TurnAngle;}}

    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;

	Collider2D collider;
	Rigidbody2D rigidBody2D;





	// Use this for initialization
	void Start () {

		collider = GetComponent<Collider2D> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();

		vehicle = new Vehicle (transform.position,collider.bounds.size);


	}

	// Update is called once per frame
	void Update () {



		// If not paused
		if (Time.timeScale == 1) {

			if (Input.GetKey (this.moveUp)) {
				vehicle.Accelerate ();
			} else if (Input.GetKey (this.moveDown)) {
				vehicle.AccelerateBack ();
			}

			if (Input.GetKey (this.moveLeft)) {
				vehicle.TurnLeft ();
			} else if (Input.GetKey (this.moveRight)) {
				vehicle.TurnRight ();
			}
			else {
				vehicle.SetWheelAngle (0);
			}

			vehicle.Action ();

			int k = 60;

			rigidBody2D.velocity = new Vector3 ((float)(Math.Sin (vehicle.Direction) * vehicle.Speed)* k, (float)(Math.Cos (vehicle.Direction) * vehicle.Speed)*k);

			//transform.position = vehicle.Origin;
			transform.eulerAngles = new Vector3(0, 0, (float)(180-Mathf.Rad2Deg*vehicle.Direction));
		}
	}
		

}
