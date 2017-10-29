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
  public int firstActiveTick = 0;
  public int lastActiveTick  = 160000;

  public bool isSaving = true; // true to create new memories, false to replay
                               // old ones

  // Use this for initialization
  void Awake() {
    state          = Memory.MemoryEvent.reposition;
    hashtable      = new Hashtable();
    lastActiveTick = 160000;
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
    state = memory.memoryEvent();

    if (state == Memory.MemoryEvent.reposition) {
      position = memory.position();
    } else {
      progress = memory.progress();
    }
  }

  public void SetInactive(Tuple3I pos = null) {
    if (pos == null) {
      pos = position;
    }
    memory = new Memory(Memory.MemoryEvent.inactive, pos);
  }

  public void deltaPosition(int firstDelta, int secondDelta, int thirdValue) {
    // Check for validity of new position here

    Tuple3I newPosition = new Tuple3I(
      position.first + firstDelta,
      position.second + secondDelta,
      thirdValue
      );

    if (MapGenerator.isValidMove(position, newPosition)) {
      position = newPosition;
    }
  }
}
