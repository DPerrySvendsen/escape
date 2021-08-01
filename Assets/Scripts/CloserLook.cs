using UnityEngine;

public class CloserLook : MonoBehaviour {

  public Vector3 cameraTargetPosition;
  public Vector3 cameraTargetRotation;

  private Movement movementController;
  private bool zoomedIn = false;

  public void Start () {
    movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
  }

  public void ZoomIn () {
    zoomedIn = true;
    movementController.ZoomCameraTo(cameraTargetPosition, cameraTargetRotation);
  }

  public void Update () {
    if (zoomedIn && Input.GetMouseButtonDown(1)) {
      zoomedIn = false;
      movementController.ResetCamera();
    }
  }
}
