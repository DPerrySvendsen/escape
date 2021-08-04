using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

  public bool  movementEnabled      = true;
  public float movementSpeed        = 5;
  public float rotationSpeedX       = 5;
  public float rotationSensitivityX = 0.5f;
  public float rotationBoundsY      = 75;
  public float rotationSmoothingY   = 0.05f;

  public float mouseOffsetX;
  public float mouseOffsetY;

  private CharacterController characterController;
  private Coroutine cameraCoroutine;

  private enum SpaceType {
    World,
    Local
  };

  public void Start () {
    characterController = GetComponent<CharacterController>();      
  }

  public void FixedUpdate () {
    if (!movementEnabled) {
      return;
    }

    if (!characterController.isGrounded) {
      characterController.Move(-transform.up * 0.1f);
    }

    characterController.Move(Input.GetAxis("Vertical")   * movementSpeed * Time.deltaTime * transform.forward);
    characterController.Move(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime * transform.right);
    mouseOffsetX = (Input.mousePosition.x - Screen.width  / 2) / Screen.width  * 2;
    mouseOffsetY = (Input.mousePosition.y - Screen.height / 2) / Screen.height * 2;
    
    if (Mathf.Abs(mouseOffsetX) > rotationSensitivityX) {
      mouseOffsetX = (mouseOffsetX - rotationSensitivityX * Mathf.Sign(mouseOffsetX)) * 2f;
    }
    else {
      mouseOffsetX = 0;
    }

    transform.RotateAround(
      transform.position,
      transform.up,
      mouseOffsetX * rotationSpeedX
    );

    Camera.main.transform.localEulerAngles = new Vector3(
      Mathf.LerpAngle(
        Camera.main.transform.localEulerAngles.x,
        -mouseOffsetY * rotationBoundsY,
        rotationSmoothingY
      ),
      0.0f,
      0.0f
    );
  }

  public void ZoomCameraTo (Vector3 targetCameraPosition, Vector3 targetCameraRotation) {
    if (cameraCoroutine != null) {
      StopCoroutine(cameraCoroutine);
    }
    movementEnabled = false;
    cameraCoroutine = StartCoroutine(ZoomCameraTo(SpaceType.World, targetCameraPosition, targetCameraRotation, 0.5f));
  }

  public void ResetCamera () {
    if (cameraCoroutine != null) {
      StopCoroutine(cameraCoroutine);
    }
    cameraCoroutine = StartCoroutine(ZoomCameraTo(SpaceType.Local, Vector3.zero, Vector3.zero, 0.5f));
  }

  private void SetPositionInSpace (SpaceType spaceType, Transform transform, Vector3 position, Vector3 rotation) {
    switch (spaceType) {
      case SpaceType.Local:
        Camera.main.transform.localPosition = position;
        Camera.main.transform.localEulerAngles = rotation;
        break;
      case SpaceType.World:
        Camera.main.transform.position = position;
        Camera.main.transform.eulerAngles = rotation;
        break;
    }
  }

  private IEnumerator ZoomCameraTo (
    SpaceType spaceType,
    Vector3 targetCameraPosition, 
    Vector3 targetCameraRotation, 
    float duration
  ) {
    float startTime = Time.time;

    Vector3 initialCameraPosition = Camera.main.transform.position;
    Vector3 initialCameraRotation = Camera.main.transform.eulerAngles;
    if (spaceType == SpaceType.Local) {
      initialCameraPosition = Camera.main.transform.localPosition;
      initialCameraRotation = Camera.main.transform.localEulerAngles;
    }

    while (Time.time < startTime + duration) {
      float ratio = (Time.time - startTime) / duration;
      SetPositionInSpace(
        spaceType,
        Camera.main.transform,
        Vector3.Lerp(initialCameraPosition, targetCameraPosition, ratio),
        new Vector3(
          Mathf.LerpAngle(initialCameraRotation.x, targetCameraRotation.x, ratio),
          Mathf.LerpAngle(initialCameraRotation.y, targetCameraRotation.y, ratio),
          Mathf.LerpAngle(initialCameraRotation.z, targetCameraRotation.z, ratio)
        )
      );
      yield return new WaitForFixedUpdate();
    }

    SetPositionInSpace(
      spaceType,
      Camera.main.transform,
      targetCameraPosition,
      targetCameraRotation
    );

    if (spaceType == SpaceType.Local && targetCameraPosition == Vector3.zero) {
      movementEnabled = true;
      GetComponent<Interaction>().CanInteract = true;
    }
    cameraCoroutine = null;
  }
}
