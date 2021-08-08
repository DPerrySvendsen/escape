using UnityEngine;

public class OpenDraw : MonoBehaviour {

  public Animator animatorDraw;

  private CloserLook closerLook;

  public void Start () {
    animatorDraw.speed = 0;
    closerLook = GetComponent<CloserLook>();
  }

  public void Open () {
    Camera.main.GetComponent<HUD>().DisplayMessage("The doors swing open...");
    closerLook.ZoomIn();
    closerLook.ZoomOutAutomaticallyAfterDelay(2.0f);
    GetComponent<SelectableObject>().Description = "With the correct code entered, the cabinet draw is now unlocked.";
    animatorDraw.speed = 0.5f;
  }
}
