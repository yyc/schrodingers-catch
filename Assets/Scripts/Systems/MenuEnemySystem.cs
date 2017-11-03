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
    Debug.Log("entered");
    Debug.Log(other.gameObject);

    if (other.gameObject.CompareTag("Player")) {
      Debug.Log("tagged");
      SceneManager.LoadScene(transitionScene);
    }
  }
}
