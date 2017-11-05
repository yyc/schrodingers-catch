using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeComponent : MonoBehaviour {
  public int chargesLeft = 3;

  public GameObject chargesBar;
  ChargeManager chargeManager;

  // Use this for initialization
  void Start() {
    chargeManager = chargesBar.GetComponent<ChargeManager>();
  }

  // Update is called once per frame
  void Update() {
    chargeManager.numCharges = chargesLeft;
  }
}
