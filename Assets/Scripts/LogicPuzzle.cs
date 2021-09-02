using UnityEngine;

public class LogicPuzzle : MonoBehaviour {

  public CloserLook closerLook;

  private bool puzzleIsEnabled = false;

  public void Start () {
    PuzzleIsEnabled = false;
  }

  public void Update () {
    if (closerLook.IsFocused != puzzleIsEnabled) {
      PuzzleIsEnabled = closerLook.IsFocused;
    }
  }

  private bool PuzzleIsEnabled {
    get {
      return puzzleIsEnabled;
    }
    set {
      puzzleIsEnabled = value;
      foreach (BoxCollider box in GetComponentsInChildren<BoxCollider>()) {
        box.enabled = puzzleIsEnabled;
      }
      GetComponent<BoxCollider>().enabled = !puzzleIsEnabled;
    }
  }
}
