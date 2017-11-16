using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButtonComponent : MonoBehaviour {

	public GameObject panel;
	public bool open = true;

	// Use this for initialization
	void Start () {
		Button button = GetComponent<Button>();
		button.onClick.AddListener(Trigger);
	}

	void Trigger() {
		panel.SetActive(open);
	}	
}
