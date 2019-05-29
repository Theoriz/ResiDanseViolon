using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeToColor : MonoBehaviour {

    [Range(0,1)]
    public float FadeValue = 0.0f;
    public Color FadeColor = Color.black;

    private Material _fadingMaterial;

    private void OnValidate()
    {
        if (_fadingMaterial)
            ChangeFadeColor(FadeColor);
    }

    private void Awake()
    {
        _fadingMaterial = new Material(Shader.Find("Unlit/FadeToColor"));
        _fadingMaterial.SetVector("_FadingColor", FadeColor);
        _fadingMaterial.SetFloat("_FadingPercent", FadeValue);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _fadingMaterial.SetFloat("_FadingPercent", 1.0f - FadeValue);

        Graphics.Blit(source, destination, _fadingMaterial);
    }

    public void ChangeFadeColor(Color newColor)
    {
        FadeColor = newColor;
        _fadingMaterial.SetVector("_FadingColor", FadeColor);
    }
}
