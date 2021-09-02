using UnityEngine;

public class CycleMaterial : MonoBehaviour {

  public Material materialTransparent;
  public Material materialTick;
  public Material materialCross;

  private MeshRenderer meshRenderer;
  private int meshIndex = 0;

  public void Start () {
    meshRenderer = GetComponent<MeshRenderer>();
    meshRenderer.material = materialTransparent;
  }

  public void Cycle () {
    switch (meshIndex) {
      case 0: meshRenderer.material = materialTick; break;
      case 1: meshRenderer.material = materialCross; break;
      case 2: meshRenderer.material = materialTransparent; break;
    }
    meshIndex++;
    if (meshIndex == 3) {
      meshIndex = 0;
    }
  }
}
