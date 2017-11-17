using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEnemySystem : MonoBehaviour {
  public string transitionScene = "";
  public float minimumLoadingTime = 3.0f;
  public Text loadingText;

  float startTime;
  Timekeeper timekeeper;
  AsyncOperation menuLoad;
  string originalText;
  bool hasEntered = false;

  void Start() {
    if(loadingText != null) {
      originalText = loadingText.text;
    }
  }
  void Update() {
    if(hasEntered == true) {
      if((timekeeper.getTime() - startTime) > 0.90 * minimumLoadingTime && menuLoad == null) {
        Debug.Log("canLoad");
        menuLoad = SceneManager.LoadSceneAsync(transitionScene);
      }
      float progress = (Mathf.Min((timekeeper.getTime() - startTime) / minimumLoadingTime, 0.9f) + 
            (0.1f * (menuLoad == null ? 0 : menuLoad.progress))) * 100;
      loadingText.text = "Loading " + Mathf.RoundToInt(progress) + "%";
    }
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    // Check the provided Collider2D parameter other to see if it is tagged
    // "PickUp", if it is...

    if (other.gameObject.CompareTag("Player")) {
      timekeeper = Timekeeper.getInstance();
      startTime = timekeeper.getTime();
      hasEntered = true;
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    Debug.Log("Exited");
    if (other.gameObject.CompareTag("Player")) {
      loadingText.text = originalText;
      hasEntered = false;
    }
  }
}
