using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEnemySystem : MonoBehaviour {
  public string transitionScene = "";

  void OnTriggerEnter2D(Collider2D other)
  {
    // Check the provided Collider2D parameter other to see if it is tagged
    // "PickUp", if it is...

    if (other.gameObject.CompareTag("Player")) {
      SceneManager.LoadScene(transitionScene);
    }
  }
}
