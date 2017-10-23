using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObservingSystem : MonoBehaviour {
  public GameObject[] twins;

  protected SpriteRenderer spriteRenderer;

  // Use this for initialization
  void Start()  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update() {}

  void OnTriggerEnter2D(Collider2D other)
  {
    // Check the provided Collider2D parameter other to see if it is tagged
    // "PickUp", if it is...
    if (other.gameObject.CompareTag("Player")) {
      foreach (GameObject twin in twins) {
        twin.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
      }
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    spriteRenderer.color = new Color(1, 1, 1, 1);
  }
}
