using System.Collections;
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
  MemoryComponent memComponent;
  SpriteRenderer spriteRenderer;

  // Use this for initialization
  void Start() {
    memComponent   = GetComponent<MemoryComponent> ();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void FixedUpdate() {
    switch (memComponent.state) {
    case Memory.MemoryEvent.reposition:
      spriteRenderer.color = Color.white;
      transform.position   =
        MapGenerator.PositionFor(memComponent.position, -1);

      transform.rotation = directions[memComponent.position.third];
      break;

    case Memory.MemoryEvent.appearing:
      spriteRenderer.color = new Color(1, 1, 1, memComponent.progress);
      break;

    case Memory.MemoryEvent.disappearing:
      spriteRenderer.color = new Color(1, 1, 1, 1.0f - memComponent.progress);
      break;
    }
  }

  public void startTransition(float time) {}
}
