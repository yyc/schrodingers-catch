using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class EnemyObservingSystem : MonoBehaviour {
  public GameObject[] twins;
  public TupleI observed;

  protected SpriteRenderer spriteRenderer;

  // Use this for initialization
  void Start()  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update() {
    spriteRenderer.color = new Color(1,
                                     1,
                                     1, 1.0f -
                                     (float)observed.first / observed.second);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    // Check the provided Collider2D parameter other to see if it is tagged
    // "PickUp", if it is...
    if (other.gameObject.CompareTag("Player")) {
      observed.first += 1;
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.CompareTag("Player")) {
      observed.first -= 1;
    }
  }
}
