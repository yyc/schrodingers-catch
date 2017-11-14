using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class EnemyObservingSystem : MonoBehaviour {
  public GameObject[] twins;
  public TupleI observed;
  public EnemySpawnSystem spawnSystem;

  protected SpriteRenderer spriteRenderer;
  public ChargeComponent chargeComponent;

  public Sprite disappearSprite;
  public RuntimeAnimatorController disappearAnimation;

  // Counts the number of observers and only updates the fraction when
  // Transitioning from unobserved
  protected int observerCount = 0;

  // Use this for initialization
  void Start()  {
    foreach (Transform child in transform) {
      spriteRenderer = child.GetComponent<SpriteRenderer>();
    }
  }

  // Update is called once per frame
  void Update() {
    if (spriteRenderer == null) {
      return;
    }

    if (observed.first == observed.second) {
      spriteRenderer.color = new Color(1, 1, 1, 1.0f);
      int tick                     = Timekeeper.getInstance().getTick();
      MemoryComponent memComponent = GetComponent<MemoryComponent>();

      if ((tick <= memComponent.firstActiveTick) ||
          (tick >= memComponent.lastActiveTick)) {
        return;
      }
      memComponent.lastActiveTick = tick;

      memComponent.SetInactive();
      GetComponent<MemorySystem>().ImmediateSave();

      memComponent.enabled                   = false;
      GetComponent<MovementSystem>().enabled = false;
      this.enabled                           = false;

      GetComponent<AnimatorChildComponent>().animator.runtimeAnimatorController =
        disappearAnimation;
      spriteRenderer.sprite = disappearSprite;

      chargeComponent.chargesLeft++;
    } else {
      spriteRenderer.color = new Color(1,
                                       1,
                                       1, 1.0f -
                                       (float)observed.first / observed.second);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    // Check the provided Collider2D parameter other to see if it is tagged
    // "PickUp", if it is...

    if (other.gameObject.CompareTag("Player")) {
      if (observerCount == 0) {
        observed.first += 1;
      }
      observerCount++;
    } else if (other.gameObject.CompareTag("Portal")) {
      Debug.Log("Game Over, man, Game Over");
      GameOverSystem.GameOver();
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.CompareTag("Player")) {
      if (observerCount == 1) {
        observed.first -= 1;
      }
      observerCount--;
    }
  }
}
