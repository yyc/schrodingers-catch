using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class EnemyControllerSystem : MonoBehaviour {
  // Use this for initialization
  public float speed    = 3.0f;
  public float fadeTime = 2f;
  private MemoryComponent memComponent;

  private static Tuple3I[] directions = new Tuple3I[4] {
    new Tuple3I(0, 1, 2),  // up
    new Tuple3I(-1, 0, 3), // left
    new Tuple3I(0, -1, 4), // down
    new Tuple3I(1, 0, 1),  // right
  };

  void Start() {
    memComponent          = GetComponent<MemoryComponent>();
    memComponent.isSaving = false;
    generatePath();
  }

  void generatePath() {
    Timekeeper timekeeper    = Timekeeper.getInstance();
    Tuple3I    position      = memComponent.position;
    int        startTick     = timekeeper.getTick();
    int        moveTick      = timekeeper.GetTickIn(fadeTime);
    float      tickIncrement = 1.0f / (moveTick - startTick);
    float      progress      = 0;
    Memory     memory;

    // Add first reposition memory
    memory = new Memory(Memory.MemoryEvent.reposition,
                        position);
    memComponent.hashtable[moveTick] = memory;

    // Create appearing animation
    for (int tick = startTick + 1; tick <= moveTick; tick++) {
      memory = new Memory(Memory.MemoryEvent.appearing,
                          progress += tickIncrement);
      memComponent.hashtable[tick] = memory;
    }

    int speedTicks = timekeeper.GetTickIn(speed) - timekeeper.getTick();

    moveTick++;

    // Add subsequent reposition memories
    while (MapGenerator.GetPathValueFor(position) != 1) {
      moveTick += speedTicks;
      if ((moveTick - startTick) / speedTicks > 1000) { // more than 1000 steps
        Debug.Log("Path taking too long!");
        return;
      }
      position = NextPathFor(position);
      memory   = new Memory(Memory.MemoryEvent.reposition,
                            position);
      memComponent.hashtable[moveTick] = memory;

    }

    // Add animation for climbing the ladder
    position.third = 1;
    Tuple3I newPosition = position + new Tuple3I(1,0,0);
    for(int i = 0; i < 16; i++) {
      memory = new Memory(Memory.MemoryEvent.reposition, position, newPosition, i * 1.0f / 16);
      memComponent.hashtable[moveTick + i] = memory; 
    }
  }

  Tuple3I NextPathFor(Tuple3I position) {
    int leastIndex        = 0;
    Tuple3I nextPosition  = position;
    int     leastDistance = 99999;
    int chosenIndex = 0;

    for (int i = 0; i < 4; i++) {
      nextPosition = position + directions[i];

      if (!MapGenerator.isValidMove(position.first, position.second,
                                    nextPosition.first, nextPosition.second)) {
        continue;
      }

      if (MapGenerator.GetPathValueFor(nextPosition) < leastDistance) {
        leastIndex = i;

        leastDistance = MapGenerator.GetPathValueFor(nextPosition);
      }
    }

    chosenIndex = leastIndex;
    for (int i = 0; i < 4; i++) {
      nextPosition = position + directions[i];

      if (!MapGenerator.isValidMove(position.first, position.second,
                                    nextPosition.first, nextPosition.second)) {
        continue;
      }

      if (i != leastIndex && Random.Range(0.0f, 8.0f) < 1.0f) {
        chosenIndex = i;
      }
    }


    nextPosition       = position + directions[chosenIndex];
    nextPosition.third = chosenIndex;
    return nextPosition;
  }

  // Update is called once per frame
  // No Update, all{{}} paths are generated at runtime
  //  void Update() {}
}
