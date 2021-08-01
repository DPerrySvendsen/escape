using UnityEngine;

public class LightSwitch : MonoBehaviour {

  public Light lightbulb;
  public SelectableObject selectableObject;

  public void Toggle () {
    lightbulb.enabled = !lightbulb.enabled;
    Camera.main.GetComponent<HUD>().DisplayMessage("The lamp blinks " + (lightbulb.enabled ? "on" : "off") + ".");
  }
}
