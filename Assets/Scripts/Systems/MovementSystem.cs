using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour {

	public static Quaternion[] directions =  new Quaternion[4] { // [right, up, left, down]
		Quaternion.AngleAxis (0, new Vector3 (0, 0, 1)),
		Quaternion.AngleAxis (90, new Vector3 (0, 0, 1)),
		Quaternion.AngleAxis (180, new Vector3 (0, 0, 1)),
		Quaternion.AngleAxis (270, new Vector3 (0, 0, 1))
	};
	MemoryComponent memComponent;

	// Use this for initialization
	void Start () {
		memComponent = GetComponent<MemoryComponent> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = MapGenerator.PositionFor (memComponent.position, -1);
		transform.rotation = directions [memComponent.position.third];

	}
}
