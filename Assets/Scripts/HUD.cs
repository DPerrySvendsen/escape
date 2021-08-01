using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

  public Text text;
  public float fadeDuration;

  private float lastUpdate;

  public void Start () {
    text = GetComponentInChildren<Text>();
  }

  public void FixedUpdate () {
    float fadeStage = (Time.time - lastUpdate) / fadeDuration;
    if (fadeStage <= 1.0f) {
      SetTextOpacity(1.0f - fadeStage);
    }
  }

  public void DisplayMessage (string message) {
    text.text = message;
    SetTextOpacity(1);
    lastUpdate = Time.time;
  }

  private void SetTextOpacity (float opacity) {
    Color colour = text.color;
    colour.a = opacity;
    text.color = colour;
  }
}
