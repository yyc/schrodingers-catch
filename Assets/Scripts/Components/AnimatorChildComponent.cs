using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorChildComponent : MonoBehaviour {
  public Animator animator;

  // Use this for initialization
  void Start() {
    foreach (Transform child in transform) {
      animator = child.GetComponent<Animator>();
    }
  }
}
