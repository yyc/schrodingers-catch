using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem : MonoBehaviour {
  public static GameOverSystem goSystem;
  public GameObject gameOverPanel;
  public AudioClip gameOverSound;
  AudioSource audioSource;

  bool isGameOver = false;

  // Use this for initialization
  void Start() {
    goSystem = this;
    isGameOver = false;
  }

  void TriggerGameOver() {
    if(isGameOver) {
      return;
    }
    GetComponent<PlayerControllerSystem>().underControl = false;
    audioSource = GetComponent<AudioSource>();
    audioSource.Stop();
    audioSource.clip = gameOverSound;
    StartCoroutine(ShowGameOverScreen());
  }

  IEnumerator ShowGameOverScreen(){
    yield return new WaitForSeconds(1);
    PauseSystem pauseSystem = GetComponent<PauseSystem>();
    pauseSystem.Pause();
    audioSource.Play();
    audioSource.loop = false;
    gameOverPanel.SetActive(true);
  }

  public static void GameOver() {
    goSystem.TriggerGameOver();
  }
}
