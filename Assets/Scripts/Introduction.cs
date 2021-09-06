using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Introduction : MonoBehaviour {

  public string[] messages;
  public SpriteRenderer controlsImage;

  private int index = 0;
  private Text textField;
  private bool canProgress = false;

  public void Start () {
    textField = GetComponentInChildren<Text>();
    StartCoroutine(FadeText(0.1f, 1.0f));
  }

  public void Update () {
    if (canProgress && Input.GetMouseButtonDown(0)) {
      if (index == messages.Length) {
        StartCoroutine(ShowControls());
      }
      else {
        StartCoroutine(FadeText(1.0f, 1.0f));
      }
    }
  }

  private IEnumerator FadeText (float duration1, float duration2) {
    float startTime = Time.time;
    Color colour = textField.color;
    canProgress = false;

    while (Time.time < startTime + duration1) {
      colour.a = 1 - (Time.time - startTime) / duration1;
      textField.color = colour;
      yield return new WaitForFixedUpdate();
    }
    colour.a = 0;
    textField.color = colour;
    textField.text = messages[index].Replace("\\n", "\n\n");
    index++;

    startTime = Time.time;
    while (Time.time < startTime + duration2) {
      colour.a = (Time.time - startTime) / duration2;
      textField.color = colour;
      yield return new WaitForFixedUpdate();
    }
    colour.a = 1;
    textField.color = colour;

    canProgress = true;
  }

  private IEnumerator ShowControls () {
    float startTime = Time.time;
    Color colour = textField.color;
    canProgress = false;

    while (Time.time < startTime + 1.0f) {
      colour.a = (Time.time - startTime) / 1.0f;
      controlsImage.color = colour;
      yield return new WaitForFixedUpdate();
    }
    colour.a = 1;
    controlsImage.color = colour;

    yield return new WaitForSeconds(2);

    while (!Input.GetMouseButtonDown(0)) {
      yield return new WaitForFixedUpdate();
    }
    SceneManager.LoadScene("Main");

  }
}
