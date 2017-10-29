using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;


// Stores and replays the object's position and actions
public class MemorySystem : MonoBehaviour {
  private MemoryComponent memComponent;

  // Use this for initialization
  void Start() {
    memComponent = GetComponent<MemoryComponent> ();
  }

  // Update is called once per frame
  void Update() {
    int tick = Timekeeper.getInstance().getTick();

    if ((tick < memComponent.firstActiveTick) ||
        (tick > memComponent.lastActiveTick)) {
      return;
    }

    if (memComponent.hashtable.ContainsKey(tick)) {
      memComponent.memory = (Memory)memComponent.hashtable[tick];
    } else if (memComponent.memory != null) {
      memComponent.hashtable[tick] = memComponent.memory;
    }
  }

  public void ImmediateSave(int tick = -100) {
    if (tick == -100) {
      tick = Timekeeper.getInstance().getTick();
    }
    Debug.Log(memComponent.memory);
    memComponent.hashtable[tick] = memComponent.memory;
  }
}
