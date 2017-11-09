using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class MemoryComponent : MonoBehaviour {
  public Tuple3I position;
  public Tuple3I destPosition;
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
    if (destPosition == null) {
      destPosition = position;
    }
    state          = Memory.MemoryEvent.reposition;
    hashtable      = new Hashtable();
    lastActiveTick = 160000;
  }

  // Update is called once per frame
  void FixedUpdate() {
    if (isSaving == true) {
      if (state == Memory.MemoryEvent.reposition) {
        memory = new Memory(Memory.MemoryEvent.reposition,
                            position,
                            destPosition,
                            progress);
      } else {
        memory = new Memory(state, progress);
      }
      return;
    }

    if (memory == null) {
      return;
    }
    state = memory.memoryEvent();

    progress = memory.progress();

    if (state == Memory.MemoryEvent.reposition) {
      position     = memory.position();
      destPosition = memory.destPosition();
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
      destPosition = newPosition;
      progress     = 0f;
    } else {
      // Even if it's not a valid move, we can still turn in that direction
      position = new Tuple3I(
        position.first,
        position.second,
        thirdValue
        );
    }
  }

  public void advance(float x) {
    memory = memory.advance(x);
  }

  public bool IsActive() {
    int time = Timekeeper.getInstance().getTick();

    return time > firstActiveTick && time < lastActiveTick;
  }
}
