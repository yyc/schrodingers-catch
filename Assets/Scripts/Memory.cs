using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class Memory : Tuple4<int, Tuple3I, Tuple3I, float>{
  public enum MemoryEvent {
    reposition,   // tuple holds new (x,y,orientation)
    appearing,    // tuple holds only (animationProgress, null, null)
    disappearing, // tuple holds (progress, null, null),
    inactive,     // tuple holds position (x, y, orientation)
  };

  public Memory(MemoryEvent memEvent, float progress) :
    base(
      (int)memEvent,
      null,
      null,
      progress
      )
  {}

  public Memory(MemoryEvent memEvent, Tuple3I second, Tuple3I third,
                float fourth) :
    base((int)memEvent, second, third, fourth) {}

  public Memory(int first, Tuple3I second, Tuple3I third, float fourth) :
    base(first, second, third, fourth) {}

  public Memory(MemoryEvent memEvent, Tuple3I tuple) :
    base(
      (int)memEvent,
      tuple,
      tuple,
      0
      ) {}

  public MemoryEvent memoryEvent() {
    return (MemoryEvent)this.first;
  }

  public Tuple3I position() {
    return this.second;
  }

  public Tuple3I destPosition() {
    return this.third;
  }

  public float progress() {
    return this.fourth;
  }

  public Memory advance(float amount) {
    if (this.second == this.third) {
      return this;
    }
    Memory newMemory = new Memory(this.first,
                                  this.second,
                                  this.third,
                                  Mathf.Min(1.0f, this.fourth += amount));
    return newMemory;
  }
}
