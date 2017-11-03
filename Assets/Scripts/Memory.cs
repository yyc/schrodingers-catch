using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class Memory : Tuple<int, Tuple3I>{
  public enum MemoryEvent {
    reposition,   // tuple holds new (x,y,orientation)
    appearing,    // tuple holds only (animationProgress, null, null)
    disappearing, // tuple holds (progress, null, null),
    inactive,     // tuple holds position (x, y, orientation)
  };

  public Memory(MemoryEvent memEvent, int row, int col, int orientation) :
    base(
      (int)memEvent,
      new Tuple3I(row, col, orientation)
      )
  {}

  public Memory(MemoryEvent memEvent, float progress) :
    base(
      (int)memEvent,
      new Tuple3I(Mathf.RoundToInt(progress * 100), 0, 0)
      )
  {}

  public Memory(MemoryEvent memEvent, Tuple3I tuple) :
    base(
      (int)memEvent,
      tuple
      ) {}

  public MemoryEvent memoryEvent() {
    return (MemoryEvent)this.first;
  }

  public Tuple3I position() {
    return this.second;
  }

  public float progress() {
    return ((float)this.second.first) / 100;
  }
}
