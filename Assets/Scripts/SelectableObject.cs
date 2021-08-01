using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour {

  public string description;
  public UnityEvent interactionEvent;

  private bool isSelected;
  private MeshRenderer meshRenderer;

  private Material[] originalMaterials;
  private Material[] selectedMaterials;

  public void Start () {
    meshRenderer = GetComponent<MeshRenderer>();
    originalMaterials = meshRenderer.materials;
    selectedMaterials = meshRenderer.materials;
    for (int i = 0; i < selectedMaterials.Length; i++) {
      selectedMaterials[i] = Resources.Load<Material>("Materials/Selected");
    }
  }

  public bool IsSelected {
    get {
      return isSelected;
    }
    set {
      isSelected = value;
      meshRenderer.materials = isSelected && HasInteractionEvent ? selectedMaterials : originalMaterials;
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
