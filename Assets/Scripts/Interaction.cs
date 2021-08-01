using UnityEngine;

public class Interaction : MonoBehaviour {

  public int raycastLength;

  private SelectableObject currentSelection;

  public void Update () {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, raycastLength)) {
      SelectableObject selectableObject = hit.collider.GetComponent<SelectableObject>();
      if (selectableObject) {
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

}
