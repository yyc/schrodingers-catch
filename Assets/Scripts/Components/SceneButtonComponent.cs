using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneButtonComponent : MonoBehaviour {
  public string sceneName;

  // Use this for initialization
  void Start() {
    Button btn = GetComponent<Button>();

    btn.onClick.AddListener(TransitionOnClick);
  }

  // Update is called once per frame
  void TransitionOnClick() {
    SceneManager.LoadScene(sceneName);
  }
}
