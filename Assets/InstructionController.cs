using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionController : MonoBehaviour {
  public List<GameObject>panels;
  public Button nextButton;
  public Button prevButton;
  public int panelIndex = 0;

  // Use this for initialization
  void Start()  {
    UpdatePanels();
    nextButton.onClick.AddListener(Next);
    prevButton.onClick.AddListener(Previous);
  }

  // Update is called once per frame
  void UpdatePanels() {
    for (int i = 0; i < panels.Count; i++) {
      panels[i].SetActive(i == panelIndex);
    }

    if (panelIndex == 0) {
      prevButton.gameObject.SetActive(false);
    } else if (panelIndex == panels.Count - 1) {
      nextButton.gameObject.SetActive(false);
    } else {
      nextButton.gameObject.SetActive(true);
      prevButton.gameObject.SetActive(true);
    }
  }

  void Next() {
    if (panelIndex == panels.Count - 1) {
      return;
    }
    panelIndex++;
    UpdatePanels();
  }

  void Previous() {
    if (panelIndex == 0) {
      return;
    }
    panelIndex--;
    UpdatePanels();
  }
}
