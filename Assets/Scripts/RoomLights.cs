using UnityEngine;

public class RoomLights : MonoBehaviour {

  public Light[] lights;

  public void Toggle () {
    lights[0].enabled = !lights[0].enabled;
    foreach (Light light in lights) {
      light.enabled = lights[0].enabled;
    }
  }
}
