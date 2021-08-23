using UnityEngine;

public class HiddenMessage : MonoBehaviour {

  public Material materialPrimary;
  public Material materialSecondary;

  public Light roomLight;
  public Light uvLamp;

  private MeshRenderer meshRenderer;

  private void Start () {
    meshRenderer = GetComponent<MeshRenderer>();
  }

  private void Update () {
    meshRenderer.material = !roomLight.enabled && uvLamp.enabled ? materialSecondary : materialPrimary;
  }

}
