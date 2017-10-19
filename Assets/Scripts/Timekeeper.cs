using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;


// Encapsulates all behaviour related to the change of time
// All objects get their time info from this class (so it's a Singleton class)
public class Timekeeper: MonoBehaviour
{

	static Timekeeper instance;
	public Text timerText;
	//Store a reference to the UI Text component which will display the timer

	public static Timekeeper getInstance ()
	{
		if (instance == null) {
			throw new MissingReferenceException ("No Timekeeper Instance Initialized!");
		}
		return instance;
	}

	public float time = 0;
	public int tick = 0;
	private bool isRewinding = false;

	// Use this for initialization
	void Awake ()
	{
		instance = this;
		time = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!isRewinding) {
			time += Time.deltaTime;
		}

		// FP operations are faster if it's a power of 2
		// ....maybe
		tick = Mathf.RoundToInt (time * 16); 
		timerText.text = time.ToString ();
	}

	public float getTime ()
	{
		return time;
	}

	public int getTick() {
		return tick;
	}

	public void startRewind ()
	{
		isRewinding = true;
	}

	public void stopRewind ()
	{
		isRewinding = false;
	}

	public void immediateRewind (float amount)
	{
		time -= amount;
	}
}
