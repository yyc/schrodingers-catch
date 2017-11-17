using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButtonComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button button = GetComponent<Button>();
		button.onClick.AddListener(Quit);
	}
	
	// Update is called once per frame
	void Quit () {
		Application.Quit();
	}
}
