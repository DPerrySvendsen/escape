using UnityEngine;

public class SelectableObject : MonoBehaviour {

  public string description;

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
      meshRenderer.materials = isSelected ? selectedMaterials : originalMaterials;
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
  }
}
