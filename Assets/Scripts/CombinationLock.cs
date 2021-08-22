using UnityEngine;
using UnityEngine.UI;

public class CombinationLock : MonoBehaviour {

  public BoxCollider button1Up;
  public BoxCollider button2Up;
  public BoxCollider button3Up;
  public BoxCollider button4Up;
  public BoxCollider button1Down;
  public BoxCollider button2Down;
  public BoxCollider button3Down;
  public BoxCollider button4Down;

  public Text display;
  public OpenDraw openDraw;

  public int[] correctCombination;
  private int[] combination;
  private bool isInteracting = false;

  private SelectableObject selectable;
  public SelectableObject drawBehind;

  public void Start () {
    selectable = GetComponent<SelectableObject>();
    SetButtonsEnabled(isInteracting);
    combination = new int[] {0, 0, 0, 0};
  }

  public void Interact () {
    SetInteracting(true);
  }

  private void CancelInteract () {
    SetInteracting(false);
  }

  private void SetInteracting (bool value) {
    isInteracting = value;
    SetButtonsEnabled(isInteracting);
  }

  private void SetButtonsEnabled (bool isEnabled) {
    selectable.enabled = !isEnabled;
    drawBehind.enabled = !isEnabled;
    if (isEnabled && selectable.IsSelected) {
      selectable.IsSelected = false;
      drawBehind.IsSelected = false;
    }
    button1Up.enabled = isEnabled;
    button2Up.enabled = isEnabled;
    button3Up.enabled = isEnabled;
    button4Up.enabled = isEnabled;
    button1Down.enabled = isEnabled;
    button2Down.enabled = isEnabled;
    button3Down.enabled = isEnabled;
    button4Down.enabled = isEnabled;
  }

  private void UpdateDisplay () {
    display.text =
     combination[0] + " " +
     combination[1] + " " +
     combination[2] + " " +
     combination[3];
  }

  private bool CorrectCombination () {
    for (int i = 0; i < correctCombination.Length; i++) {
      if (correctCombination[i] != combination[i]) {
        return false;
      }
    }
    return true;
  }

  private void UpdateCombination (Collider collider) {
    if (collider == button1Up) {
      combination[0] = Mathf.Min(9, combination[0] + 1);
    }
    else if (collider == button2Up) {
      combination[1] = Mathf.Min(9, combination[1] + 1);
    }
    else if (collider == button3Up) {
      combination[2] = Mathf.Min(9, combination[2] + 1);
    }
    else if (collider == button4Up) {
      combination[3] = Mathf.Min(9, combination[3] + 1);
    }
    else if (collider == button1Down) {
      combination[0] = Mathf.Max(0, combination[0] - 1);
    }
    else if (collider == button2Down) {
      combination[1] = Mathf.Max(0, combination[1] - 1);
    }
    else if (collider == button3Down) {
      combination[2] = Mathf.Max(0, combination[2] - 1);
    }
    else if (collider == button4Down) {
      combination[3] = Mathf.Max(0, combination[3] - 1);
    }
    UpdateDisplay();
  }

  public void Update () {
    if (isInteracting && Input.GetMouseButtonDown(0)) {
      RaycastHit hit;
      if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {

        UpdateCombination(hit.collider);

        if (CorrectCombination()) {
          isInteracting = false;
          CancelInteract();
          openDraw.Open();
          openDraw.GetComponent<BoxCollider>().enabled = false;
        };
      }
    }

    if (isInteracting && Input.GetMouseButtonDown(1)) {
      CancelInteract();
    }
  }
}
