using UnityEngine;

public class OpenDoor : MonoBehaviour {

  public Animator animatorDoorLeft;
  public Animator animatorDoorRight;

  public bool isLocked = true;

  public void Start () {
    animatorDoorLeft.speed = 0;
    animatorDoorRight.speed = 0;
  }

  public void Open () {
    if (!isLocked) {
      animatorDoorLeft.speed = 1;
      animatorDoorRight.speed = 1;
    }
  }
}