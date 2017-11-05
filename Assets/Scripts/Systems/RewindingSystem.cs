using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindingSystem : MonoBehaviour {
  public float rewindingSpeed = 3.0f;

  // Use this for initialization

  private PlayerControllerSystem controllerSystem;
  private Timekeeper timekeeper;
  private WaveControllerSystem waveController;
  void Start() {
    controllerSystem = GetComponent<PlayerControllerSystem>();
    timekeeper       = Timekeeper.getInstance();
    waveController   = GetComponent<WaveControllerSystem>();
  }

  // Update is called once per frame
  void Update() {
    if (controllerSystem.state != PlayerControllerSystem.State.traveling) {
      return;
    }

    if (timekeeper.getTime() <= waveController.waveStartTime) {
      // Done rewinding
      controllerSystem.underControl = true;
      controllerSystem.willStartWalking();
      return;
    }

    controllerSystem.underControl = false;

    // rewinds at the speed of rewindingSpeed;
    timekeeper.immediateOffset(Time.deltaTime * rewindingSpeed * -1);
  }
}
