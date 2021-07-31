using UnityEngine;

public class Movement : MonoBehaviour {

  public float movementSpeed;
  public float rotationSpeed;
  public float rotationSensitivityX;
  public float rotationBoundsY;

  public float mouseOffsetX;
  public float mouseOffsetY;

  private CharacterController characterController;

  public void Start () {
    characterController = GetComponent<CharacterController>();      
  }

  public void FixedUpdate () {
    characterController.Move(Input.GetAxis("Vertical")   * movementSpeed * Time.deltaTime * transform.forward);
    characterController.Move(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime * transform.right);
    mouseOffsetX = (Input.mousePosition.x - Screen.width  / 2) / Screen.width  * 2;
    mouseOffsetY = (Input.mousePosition.y - Screen.height / 2) / Screen.height * 2;
    
    if (Mathf.Abs(mouseOffsetX) > rotationSensitivityX) {
      mouseOffsetX = (mouseOffsetX - rotationSensitivityX * Mathf.Sign(mouseOffsetX));
    }
    else {
      mouseOffsetX = 0;
    }

    transform.RotateAround(
      transform.position,
      transform.up,
      mouseOffsetX * rotationSpeed
    );

    Camera.main.transform.localEulerAngles = new Vector3(
      -mouseOffsetY * rotationBoundsY,
      0.0f,
      0.0f
    );
  }
}
