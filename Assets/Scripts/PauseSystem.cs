using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour {

	public Timekeeper timekeeper;
	public PlayerControllerSystem playerControllerSystem;
	public GameObject pausePanel;
	public Button[] pauseButtons;
	public Button[] resumeButtons;

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
	}
	
	void Pause() {
		timekeeper.startRewind();
		playerControllerSystem.underControl = false;
		pausePanel.SetActive(true);
	}

	void Resume() {
		timekeeper.stopRewind();
		playerControllerSystem.underControl = true;
		pausePanel.SetActive(false);
	}

}
