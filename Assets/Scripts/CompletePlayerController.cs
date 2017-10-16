﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Unitilities.Tuples;

public class CompletePlayerController : MonoBehaviour {

	public float speed;				//Floating point variable to store the player's movement speed.
	public bool underControl = true;
	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int count;				//Integer to store the number of pickups collected so far.
	private Timekeeper timekeeper;
	private Hashtable hashtable;
	public Tuple<int, int> gridCoords;
	public float cooldown = 0.1f;
	private float lastPressTime = 0f;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Awake(){
		hashtable = new Hashtable (new FloatHash ());
		print (hashtable);
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		if (timekeeper == null) {
			timekeeper = Timekeeper.getInstance ();
		}
		if (underControl) {
			// only accept input after a cooldown
			if (timekeeper.getTime() - lastPressTime >= cooldown) {

				//Store the current horizontal input in the float moveHorizontal.
				int moveHorizontal = Mathf.RoundToInt(Input.GetAxis ("Horizontal"));

				//Store the current vertical input in the float moveVertical.
				int moveVertical = Mathf.RoundToInt(Input.GetAxis ("Vertical"));

				if (moveHorizontal != 0) {
					lastPressTime = timekeeper.getTime ();
					gridCoords = new Tuple<int, int> (gridCoords.first, gridCoords.second + moveHorizontal);
				} else if (moveVertical != 0) {
					lastPressTime = timekeeper.getTime ();
					gridCoords = new Tuple<int, int> (gridCoords.first + moveVertical, gridCoords.second);
				}

			}



			hashtable.Add (timekeeper.getTime (), gridCoords);


			//move to appropriate coordinate


		} else {
			if (hashtable.ContainsKey (timekeeper.getTime ())) {
				gridCoords = (Tuple<int, int>) hashtable [timekeeper.getTime ()];
			}
		}
		transform.position = MapGenerator.PositionFor (gridCoords, -1);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
//	void OnTriggerEnter2D(Collider2D other) 
//	{
//		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
//		if (other.gameObject.CompareTag ("PickUp")) {
//			//... then set the other object we just collided with to inactive.
//			other.gameObject.SetActive (false);
//			
//
//			Respawn (other.gameObject);
//		} else if (other.gameObject.CompareTag ("Hazard")) {
//			other.gameObject.SetActive (false);
//			
//		}
//	}
//	void Respawn(GameObject gameObject) {
//		float newX = Random.Range (-11, 11);
//		float newY = Random.Range (-11, 11);
//		Vector2 newPosition = new Vector2 (newX, newY);
//		gameObject.GetComponent<Transform> ().position = newPosition;
//
//		gameObject.SetActive (true);
//	}

}
