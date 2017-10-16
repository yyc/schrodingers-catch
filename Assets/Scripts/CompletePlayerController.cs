using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CompletePlayerController : MonoBehaviour {

	public float speed;				//Floating point variable to store the player's movement speed.
	public bool underControl = true;
	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int count;				//Integer to store the number of pickups collected so far.
	private Timekeeper timekeeper;
	private Hashtable hashtable;

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
			//Store the current horizontal input in the float moveHorizontal.
			float moveHorizontal = Input.GetAxis ("Horizontal");

			//Store the current vertical input in the float moveVertical.
			float moveVertical = Input.GetAxis ("Vertical");

			//Use the two store floats to create a new Vector2 variable movement.
			Vector2 movement = new Vector2 (moveHorizontal, moveVertical);


			hashtable.Add (timekeeper.getTime (), rb2d.position);

			//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
			rb2d.AddForce (movement * speed);


		} else {
			rb2d.velocity = new Vector2 (0, 0);
			print (hashtable.Count);
			if (hashtable.ContainsKey (timekeeper.getTime ())) {
				rb2d.position = (Vector2) hashtable [timekeeper.getTime ()];
			}
		}
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) {
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive (false);
			

			Respawn (other.gameObject);
		} else if (other.gameObject.CompareTag ("Hazard")) {
			other.gameObject.SetActive (false);
			
		}
	}
	void Respawn(GameObject gameObject) {
		float newX = Random.Range (-11, 11);
		float newY = Random.Range (-11, 11);
		Vector2 newPosition = new Vector2 (newX, newY);
		gameObject.GetComponent<Transform> ().position = newPosition;

		gameObject.SetActive (true);
	}

}
