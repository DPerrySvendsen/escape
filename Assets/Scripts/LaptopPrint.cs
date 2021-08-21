using UnityEngine;
using UnityEngine.UI;

public class LaptopPrint : MonoBehaviour {

  public Text screenText;
  public InputField input;

  private string prompt;
  private bool isInteracting;
  private bool passwordPrompt;

  private string enterPasswordPrompt = "Session timed out. Enter password to continue...";

  public void EnableInput () {
    input.ActivateInputField();
    input.Select();
    isInteracting = true;
  }

  public void DisableInput () {
    input.DeactivateInputField();
    isInteracting = false;
  }

  public void Start () {
    prompt = screenText.text;
  }

  public void Update () {
    if (Input.GetKeyDown(KeyCode.Return)) {
      if (!passwordPrompt) {
        if (input.text == "y") {
          screenText.text = prompt + "y" + "\n" + enterPasswordPrompt + "\n" + ":>";
          Vector3 position = input.transform.localPosition;
          position.y = 20;
          input.transform.localPosition = position;
          passwordPrompt = true;
        }
        else if (input.text != "n")  {
          screenText.text = prompt + "\n" + "Invalid command: " + input.text;
        }
        input.text = "";
        EnableInput();
      }
      else {
        if (input.text == "hunter2") {
          screenText.text = prompt + "y" + "\n" + enterPasswordPrompt + "\n" + ":>" + "\n" + "User authentication successful, printing document...";
          DisableInput();
        }
        else {
          screenText.text = prompt + "y" + "\n" + enterPasswordPrompt + "\n" + ":>" + "\n" + "Invalid password.";
          input.text = "";
          EnableInput();
        }

      }
    }

    if (isInteracting && Input.GetMouseButtonDown(1)) {
      DisableInput();
    }
  }
}
