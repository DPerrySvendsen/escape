using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour {

  public string description;
  public UnityEvent interactionEvent;

  private bool isSelected;
  private Outline outline;

  public void Start () {
    outline = gameObject.AddComponent<Outline>();
    outline.OutlineColor = HasInteractionEvent ? Color.cyan : Color.white;
    outline.OutlineWidth = 5f;
    outline.enabled = false;
  }

  public bool IsSelected {
    get {
      return isSelected;
    }
    set {
      isSelected = value;
      outline.enabled = isSelected;
    }
  }

  public string Description {
    get {
      if (description != "") {
        return description;
      }
      else { 
        return "Error: Description for GameObject " + name + " not found.";
      }
    }
    set {
      description = value;
    }
  }

  private bool HasInteractionEvent {
    get {
      return interactionEvent.GetPersistentEventCount() > 0;
    }
  }

  public void Interact () {
    if (HasInteractionEvent) {
      interactionEvent.Invoke();
    }
  }
}
