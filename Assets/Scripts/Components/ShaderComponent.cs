using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderComponent : MonoBehaviour {
  public float intensity = 0;
  public Material material;

  // Creates a private material used to the effect
  void Awake()
  {}

  // Postprocess the image
  void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (intensity == 0)
    {
      Graphics.Blit(source, destination);
      return;
    }

    material.SetFloat("_bwBlend", intensity);
    Graphics.Blit(source, destination, material);
  }
}
