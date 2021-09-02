using UnityEngine;

public class HiddenMessage : MonoBehaviour {

  public Material materialPrimary;
  public Material materialSecondary;

  public Light roomLight;
  public Light uvLamp;

  private MeshRenderer meshRenderer;

  public void Start () {
    meshRenderer = GetComponent<MeshRenderer>();
  }

  public void Update () {
    meshRenderer.material = !roomLight.enabled && uvLamp.enabled ? materialSecondary : materialPrimary;
  }

}
