using UnityEngine;

public class Interaction : MonoBehaviour {

  public int raycastLength;

  private SelectableObject currentSelection;

  private bool canInteract = true;

  public void Update () {
    if (!canInteract) {
      return;
    }

    RaycastHit hit;
    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, raycastLength)) {
      SelectableObject selectableObject = hit.collider.GetComponent<SelectableObject>();
      if (selectableObject && selectableObject.enabled) {
        SelectObject(selectableObject);
      }
      else {
        CancelSelection();
      }
    }
    else {
      CancelSelection();
    }

    if (Input.GetMouseButtonDown(0) && currentSelection) {
      currentSelection.Interact();
    }
  }

  private void SelectObject (SelectableObject selectableObject) {
    if (selectableObject != currentSelection) {
      CancelSelection();
      selectableObject.IsSelected = true;
      Camera.main.GetComponent<HUD>().DisplayMessage(selectableObject.Description);
      currentSelection = selectableObject;
    }
  }

  private void CancelSelection () {
    if (currentSelection) {
      currentSelection.IsSelected = false;
      currentSelection = null;
    }
  }

  public bool CanInteract {
    get {
      return canInteract;
    }
    set {
      canInteract = value;
      if (!canInteract) {
        CancelSelection();
      }
    }
  }

}
