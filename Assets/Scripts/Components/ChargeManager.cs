using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeManager : MonoBehaviour {
  public int numCharges = 4;

  // For positioning purposes
  public GameObject firstBar;
  public GameObject secondBar;
  public Sprite filledBar;
  public Sprite emptyBar;


  private int numVisible = 0;
  private Vector3 barStart;
  private float barSpacing;

  // Use this for initialization
  void Start() {
    while (transform.childCount < numCharges) {
      Transform newObj = Instantiate(firstBar.transform);
      newObj.SetParent(transform);
    }
    barStart   = firstBar.transform.position;
    barSpacing = secondBar.transform.position.x - firstBar.transform.position.x;
  }

  // Update is called once per frame
  void Update() {
    while (transform.childCount < numCharges) {
      Transform newObj = Instantiate(transform.GetChild(0));
      newObj.SetParent(transform);
    }

    if (numVisible == numCharges) {
      return;
    }
    numVisible = 0;

    for (int i = 0; i < transform.childCount; i++) {
      Transform bar = transform.GetChild(i);

      if (i < numCharges) {
        bar.GetComponent<Image>().sprite = filledBar;
        bar.position                     = barStart + new Vector3(barSpacing * i,
                                                                  0,
                                                                  0);
        numVisible++;
      } else {
        bar.GetComponent<Image>().sprite = emptyBar;
      }
    }
  }
}
