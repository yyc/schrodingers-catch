using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class MemoryComponent : MonoBehaviour {

	public Tuple3I position;
	public Memory memory;
	public Hashtable hashtable;

	public bool isSaving = true; // true to create new memories, false to replay old ones

	// Use this for initialization
	void Start () {
		position = Tuple3I.zero;
		hashtable = new Hashtable ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isSaving == true) {
			memory = new Memory (Memory.MemoryEvent.reposition, position);
		} else {
			if (memory == null) {
				return;
			}
			if (memory.memoryEvent() == Memory.MemoryEvent.reposition) {
				position = memory.position();
			}
		}
	}

	public void deltaPosition(int firstDelta, int secondDelta, int thirdValue) {
		position = new Tuple3I (
			position.first + firstDelta,
			position.second + secondDelta,
			thirdValue
		);
	}
}
