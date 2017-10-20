using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class MemoryComponent : MonoBehaviour {
  public Tuple3I position;
  public Memory.MemoryEvent state;
  public float progress = 0;
  public Memory memory;
  public Hashtable hashtable;

  public bool isSaving = true; // true to create new memories, false to replay
                               // old ones

  // Use this for initialization
  void Start() {
    state     = Memory.MemoryEvent.reposition;
    hashtable = new Hashtable();
  }

  // Update is called once per frame
  void FixedUpdate() {
    if (isSaving == true) {
      if (state == Memory.MemoryEvent.reposition) {
        memory = new Memory(Memory.MemoryEvent.reposition, position);
      } else {
        memory = new Memory(state, progress);
      }
      return;
    }

    if (memory == null) {
      return;
    }

    if (memory.memoryEvent() == Memory.MemoryEvent.reposition) {
      position = memory.position();
    } else {
      progress = memory.progress();
    }
  }

  public void deltaPosition(int firstDelta, int secondDelta, int thirdValue) {
    // Check for validity of new position here

    Tuple3I newPosition = new Tuple3I(
      position.first + firstDelta,
      position.second + secondDelta,
      thirdValue
      );

    if (MapGenerator.isValidPosition(newPosition)) {
      position = newPosition;
    }
  }
}
