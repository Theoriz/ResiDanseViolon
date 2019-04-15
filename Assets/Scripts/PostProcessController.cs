using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour {

    /******************************
     * Allow to control a PostProcessProfile (v2) from the editor or another script at runtime.
     * It modifies the profile directly.
     * 
     * To modify the profile values from script, call the corresponding UpdateEffectValue functions with the new values, 
     * or modify this script parameters then call UpdateProfileValues.
     * ******************************/

    public PostProcessProfile profile;

    [Tooltip("Should this script values be overrided by the profile values on start ?")]
    public bool LoadProfileValuesAtStart = true;

    [Header("Color Grading")]
    public float colorGradingTemperature = 0;
    public float colorGradingPostExposure = 0;
    public float colorGradingSaturation = 0;
    public float colorGradingContrast = 0;

    [Header("Bloom")]
    public float bloomIntensity = 1;
    public float bloomThreshold = 1;

    private bool hasBloom = false;
    private bool hasColorGrading = false;

    private Bloom bloomSettings;
    private ColorGrading colorGradingSettings;

    #region MonoBehaviour Functions

    private void Start()
    {
        hasBloom = profile.TryGetSettings<Bloom>(out bloomSettings);
        hasColorGrading = profile.TryGetSettings<ColorGrading>(out colorGradingSettings);

        if (LoadProfileValuesAtStart)
            LoadProfileValues();

        UpdateProfileValues();
    }

	private void OnValidate()
    {
        UpdateProfileValues();
    }

    #endregion

    #region Profile Change Functions

    public void LoadProfileValues()
    {
        if (hasBloom)
        {
            bloomIntensity = bloomSettings.intensity;
            bloomThreshold = bloomSettings.threshold;
        }

        if (hasColorGrading)
        {
            colorGradingTemperature = colorGradingSettings.temperature;
            colorGradingPostExposure = colorGradingSettings.postExposure;
            colorGradingSaturation = colorGradingSettings.saturation;
            colorGradingContrast = colorGradingSettings.contrast;
        }
    }

    public void UpdateProfileValues()
    {
        UpdateBloomIntensity(bloomIntensity);
        UpdateBloomThreshold(bloomThreshold);

        UpdateColorGradingTemperature(colorGradingTemperature);
        UpdateColorGradingPostExposure(colorGradingPostExposure);
        UpdateColorGradingSaturation(colorGradingSaturation);
        UpdateColorGradingContrast(colorGradingContrast);
    }

    /*** BLOOM ***/
    public void UpdateBloomIntensity(float newValue)
    {
        if (hasBloom)
            bloomSettings.intensity.Override(newValue);
    }

    public void UpdateBloomThreshold(float newValue)
    {
        if (hasBloom)
            bloomSettings.threshold.Override(newValue);
    }

    /*** COLOR GRADING ***/
    public void UpdateColorGradingTemperature(float newValue)
    {
        if (hasColorGrading)
            colorGradingSettings.temperature.Override(newValue);
    }

    public void UpdateColorGradingPostExposure(float newValue)
    {
        if (hasColorGrading)
            colorGradingSettings.postExposure.Override(newValue);
    }

    public void UpdateColorGradingSaturation(float newValue)
    {
        if (hasColorGrading)
            colorGradingSettings.saturation.Override(newValue);
    }

    public void UpdateColorGradingContrast(float newValue)
    {
        if (hasColorGrading)
            colorGradingSettings.contrast.Override(newValue);
    }

    #endregion
}
