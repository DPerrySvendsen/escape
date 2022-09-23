using UnityEngine;

public class OpenDoor : MonoBehaviour {

  public Animator animatorDoorLeft;
  public Animator animatorDoorRight;
  public GameObject discoLight;

  private CloserLook closerLook;

  public void Start () {
    animatorDoorLeft.speed  = 0;
    animatorDoorRight.speed = 0;
    closerLook = GetComponent<CloserLook>();
    discoLight.SetActive(false);
  }

  public void Open () {
    Camera.main.GetComponent<HUD>().DisplayMessage("The doors swing open...");
    closerLook.ZoomIn();
    closerLook.ZoomOutAutomaticallyAfterDelay(2.0f);
    string description = "With the correct code entered, the doors are now unlocked.";
     animatorDoorLeft.GetComponent<SelectableObject>().Description = description;
    animatorDoorRight.GetComponent<SelectableObject>().Description = description;
    animatorDoorLeft.speed  = 0.5f;
    animatorDoorRight.speed = 0.5f;
    discoLight.SetActive(true);
  }
}
