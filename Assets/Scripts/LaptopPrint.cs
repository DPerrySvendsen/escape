using UnityEngine;
using UnityEngine.UI;

public class LaptopPrint : MonoBehaviour {

  public Text screenText;
  public InputField input;
  public Animator printerPaper;
  public CloserLook closerLook;

  public string correctUsername;
  public string correctPassword;

  private string prompt;
  private bool isInteracting;
  private string username;

  private bool passwordPrompt;
  private bool resetPrompt;

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
    printerPaper.speed = 0;
  }

  public void Update () {
    if (Input.GetKeyDown(KeyCode.Return) && (input.text != "" || resetPrompt)) {
      if (!passwordPrompt && !resetPrompt) {
        username = input.text;
        screenText.text = prompt + username + "\n" + "Enter access code for user " + username + ": " + "\n" + ":>";
        Vector3 position = input.transform.localPosition;
        position.y = 50;
        input.transform.localPosition = position;
        input.text = "";
        passwordPrompt = true;
        EnableInput();
      }
      else {
        if (input.text == correctPassword && username == correctUsername) {
          screenText.text = prompt + username + "\n" + "Enter access code for user " + username + ":" + "\n" + ":>" + "\n" + "User authentication successful, printing document...";
          DisableInput();
          closerLook.ZoomIn();
          closerLook.ZoomOutAutomaticallyAfterDelay(2.0f);
          printerPaper.speed = 0.2f;
          enabled = false;
          input.enabled = false;
        }
        else if (resetPrompt) {
          screenText.text = prompt;
          Vector3 position = input.transform.localPosition;
          position.y = 73;
          input.transform.localPosition = position;
          resetPrompt = false;
        }
        else {
          screenText.text = prompt + username + "\n" + "Enter access code for user " + username + ":" + "\n" + ":>" + "\n" + "Invalid access code or unauthorised user. Press enter to continue...";
          username = "";
          input.text = "";
          passwordPrompt = false;
          resetPrompt = true;
        }
      }
    }

    if (isInteracting && Input.GetMouseButtonDown(1)) {
      DisableInput();
    }
  }
}
