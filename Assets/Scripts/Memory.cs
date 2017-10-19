using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class Memory : Tuple<int, Tuple3I>{
  public enum MemoryEvent {
    reposition,
    appear,
    disappear
  };

  public Memory(MemoryEvent memEvent, int row, int col, int orientation) :
    base(
      (int)memEvent,
      new Tuple3I(row, col, orientation)
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
}
