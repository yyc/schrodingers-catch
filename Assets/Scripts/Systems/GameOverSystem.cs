using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem : MonoBehaviour {
  public static GameOverSystem goSystem;
  public GameObject gameOverPanel;

  // Use this for initialization
  void Start() {
    goSystem = this;
  }

  void TriggerGameOver() {
    GetComponent<Timekeeper>().startRewind();
    GetComponent<PlayerControllerSystem>().underControl = false;
    gameOverPanel.SetActive(true);
  }

  public static void GameOver() {
    goSystem.TriggerGameOver();
  }
}
