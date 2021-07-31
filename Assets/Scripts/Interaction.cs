using UnityEngine;

public class Interaction : MonoBehaviour {

  public int raycastLength;

  private SelectableObject previousSelection;

  public void Update () {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, raycastLength)) {
      SelectableObject selectableObject = hit.collider.GetComponent<SelectableObject>();
      if (selectableObject) {
        SelectObject(selectableObject);
      }
      else {
        CancelPreviousSelection();
      }
    }
    else {
      CancelPreviousSelection();
    }
  }

  private void SelectObject (SelectableObject selectableObject) {
    if (selectableObject != previousSelection) {
      CancelPreviousSelection();
      selectableObject.IsSelected = true;
      Camera.main.GetComponent<HUD>().DisplayMessage(selectableObject.Description);
      previousSelection = selectableObject;
    }
  }

  private void CancelPreviousSelection () {
    if (previousSelection) {
      previousSelection.IsSelected = false;
      previousSelection = null;
    }
  }

}
