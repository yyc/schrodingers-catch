using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeComponent : MonoBehaviour {
  public int chargesLeft = 3;
  public int maxCharges = 10;

  public ChargeManager chargeManager;
  
  // Use this for initialization
  void Start() {
  }

  // Update is called once per frame
  void Update() {
    chargeManager.numCharges = chargesLeft;
  }

  public void Increment() {
    chargesLeft = Mathf.Min(maxCharges, chargesLeft + 1);
  }
}
