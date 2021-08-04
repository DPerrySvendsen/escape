using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {

  public string passcode;
  public OpenDoor door;

  public BoxCollider buttonA;
  public BoxCollider buttonB;
  public BoxCollider buttonC;
  public BoxCollider button0;
  public BoxCollider button1;
  public BoxCollider button2;
  public BoxCollider button3;
  public BoxCollider button4;
  public BoxCollider button5;
  public BoxCollider button6;
  public BoxCollider button7;
  public BoxCollider button8;
  public BoxCollider button9;
  public BoxCollider buttonClear;
  public BoxCollider buttonOK;

  public Text display;

  private bool isInteracting = false;

  private string combination;
  private SelectableObject selectable;

  public void Start () {
    selectable = GetComponent<SelectableObject>();
    SetButtonsEnabled(isInteracting);
  }

  private void SetButtonsEnabled (bool isEnabled) {
    selectable.enabled = !isEnabled;
    if (isEnabled && selectable.IsSelected) {
      selectable.IsSelected = false;
    }
    buttonA.enabled = isEnabled;
    buttonB.enabled = isEnabled;
    buttonC.enabled = isEnabled;
    button0.enabled = isEnabled;
    button1.enabled = isEnabled;
    button2.enabled = isEnabled;
    button3.enabled = isEnabled;
    button4.enabled = isEnabled;
    button5.enabled = isEnabled;
    button6.enabled = isEnabled;
    button7.enabled = isEnabled;
    button8.enabled = isEnabled;
    button9.enabled = isEnabled;
    buttonClear.enabled = isEnabled;
    buttonOK.enabled = isEnabled;
  }

  public void Interact () {
    SetInteracting(true);
  }

  private void CancelInteract () {
    combination = "";
    display.text = combination;
    SetInteracting(false);
  }

  private void SetInteracting (bool value) {
    isInteracting = value;
    SetButtonsEnabled(isInteracting);
  }

  private string GetButtonCharacter (Collider collider) {
    if (collider == buttonA) { return "A"; }
    if (collider == buttonB) { return "B"; }
    if (collider == buttonC) { return "C"; }
    if (collider == button0) { return "0"; }
    if (collider == button1) { return "1"; }
    if (collider == button2) { return "2"; }
    if (collider == button3) { return "3"; }
    if (collider == button4) { return "4"; }
    if (collider == button5) { return "5"; }
    if (collider == button6) { return "6"; }
    if (collider == button7) { return "7"; }
    if (collider == button8) { return "8"; }
    if (collider == button9) { return "9"; }
    return "";
  }

  public void Update () {
    if (isInteracting && Input.GetMouseButtonDown(0)) {
      RaycastHit hit;
      if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
        if (hit.collider == buttonOK && combination == passcode) {
          isInteracting = false;
          CancelInteract();
          door.Open();
        }
        else if (hit.collider == buttonClear || hit.collider == buttonOK) {
          combination = "";
        }
        else {
          combination += GetButtonCharacter(hit.collider);
        }
        display.text = combination;
      }
    }

    if (isInteracting && Input.GetMouseButtonDown(1)) {
      CancelInteract();
    }
  }
}
