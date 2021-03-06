﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the movement and animation of the object based on its memComponent
public class MovementSystem : MonoBehaviour {
  public static Quaternion[] directions =  new Quaternion[4] { // [right, up,
                                                               // left, down]
    Quaternion.AngleAxis(0, new Vector3(0, 0, 1)),
    Quaternion.AngleAxis(90, new Vector3(0, 0, 1)),
    Quaternion.AngleAxis(180, new Vector3(0, 0, 1)),
    Quaternion.AngleAxis(270, new Vector3(0, 0, 1))
  };
  public static Quaternion[] inverses =  new Quaternion[4] { // [right, up,
                                                             // left, down]
    Quaternion.AngleAxis(0, new Vector3(0, 0, 1)),
    Quaternion.AngleAxis(270, new Vector3(0, 0, 1)),
    Quaternion.AngleAxis(180, new Vector3(0, 0, 1)),
    Quaternion.AngleAxis(90, new Vector3(0, 0, 1))
  };

  MemoryComponent memComponent;
  SpriteRenderer spriteRenderer;
  Animator animator;

  // Use this for initialization
  void Start() {
    memComponent   = GetComponent<MemoryComponent> ();
    spriteRenderer = GetComponent<SpriteRenderer>();

    foreach (Transform child in transform) {
      if (child.CompareTag("Animator")) {
        spriteRenderer = child.GetComponent<SpriteRenderer>();
        animator       = child.GetComponent<Animator>();
      }
    }
  }

  // Update is called once per frame
  void FixedUpdate() {
    int tick = Timekeeper.getInstance().getTick();

    // If it's before or after the active period, byebye
    if ((tick < memComponent.firstActiveTick) ||
        (tick > memComponent.lastActiveTick)) {
      transform.position = new Vector3(-100, -100, -10);
      return;
    }


    // Else we have to update the position and opacity etc.
    switch (memComponent.state) {
    case Memory.MemoryEvent.reposition:

      if (memComponent.position == memComponent.destPosition) {
        // Staying still;
        transform.position =
          MapGenerator.PositionFor(memComponent.position, -1);
      } else { // Need to tween movement
        transform.position =
          MapGenerator.PositionFor(memComponent.position,
                                   memComponent.destPosition,
                                   memComponent.progress);
      }


      // Update sprite for rotation
      int rotation = memComponent.position.third;

      if (animator != null) {
        animator.SetInteger("Direction", memComponent.position.third);
        animator.transform.rotation = Quaternion.identity;
      }
      transform.rotation = directions[rotation];
      break;

    case Memory.MemoryEvent.appearing:
      spriteRenderer.color = new Color(1, 1, 1, memComponent.progress);
      break;

    case Memory.MemoryEvent.disappearing:
      spriteRenderer.color = new Color(1, 1, 1, 1.0f - memComponent.progress);
      break;

    case Memory.MemoryEvent.inactive:
      spriteRenderer.color = Color.clear;
      break;
    }
  }

  public void startTransition(float time) {}
}
