using UnityEngine;
using System.Collections;

public class DiscoLight : MonoBehaviour {

  public Color[] colours;

  private Light lightScript;
  private Coroutine coroutine;

  public void Start () {
    lightScript = GetComponent<Light>();
  }

  public void Update () {
    if (coroutine == null) {
      coroutine = StartCoroutine(SetColour(
        Random.Range(1.0f, 5.0f),
        colours[Random.Range(0, colours.Length)] 
      ));
    }
  }

  private IEnumerator SetColour (float delay, Color colour) {
    yield return new WaitForSeconds(delay);
    lightScript.color = colour;
    coroutine = null;
  }
}
