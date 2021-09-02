using UnityEngine;
using System.Collections;

public class CloserLook : MonoBehaviour {

  public Vector3 cameraTargetPosition;
  public Vector3 cameraTargetRotation;

  private Movement movementController;

  public void Start () {
    movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
  }

  public void ZoomIn () {
    movementController.ZoomCameraTo(cameraTargetPosition, cameraTargetRotation);
  }

  public bool IsFocused {
    get {
      return Camera.main.transform.position == cameraTargetPosition;
    }
  }

  public void ZoomOut () {
    movementController.ResetCamera();
  }

  public void Update () {
    if (IsFocused && Input.GetMouseButtonDown(1)) {
      ZoomOut();
    }
  }

  public void ZoomOutAutomaticallyAfterDelay (float delay) {
    StartCoroutine(ResetCameraAfter(delay));
  }

  private IEnumerator ResetCameraAfter (float delay) {
    yield return new WaitForSeconds(delay);
    ZoomOut();
  }
}
