using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour {

	public Timekeeper timekeeper;
	public PlayerControllerSystem playerControllerSystem;
	public Button[] pauseButtons;
	public Button[] resumeButtons;
	int semaphore = 0;

	// Use this for initialization
	void Start () {
		playerControllerSystem = GetComponent<PlayerControllerSystem>();
		timekeeper = GetComponent<Timekeeper>();
		foreach(Button button in pauseButtons) {
			button.onClick.AddListener(Pause);
		}
		foreach(Button button in resumeButtons) {
			button.onClick.AddListener(Resume);
		}
		semaphore = 0;
	}
	
	public void Pause() {
		timekeeper.startRewind();
		playerControllerSystem.underControl = false;
		semaphore++;
	}

	public void Resume() {
		semaphore --;
		if(semaphore > 0) {
			return;
		}
		timekeeper.stopRewind();
		playerControllerSystem.underControl = true;
	}

}
