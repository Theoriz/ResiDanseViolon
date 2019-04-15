using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessControllerControllable : Controllable
{
    /* This script should be on the same object as the PostProcessManager script and will automatically control it */

    private PostProcessController controller;

    [OSCProperty]
    public bool LoadProfileValuesAtStart = true;

    [Header("Color Grading")]
    [OSCProperty]
    public float colorGradingPostExposure = 0;
    [OSCProperty]
    [Range(-100,100)]
    public float colorGradingTemperature = 0;
    [OSCProperty]
    [Range(-100, 100)]
    public float colorGradingSaturation = 0;
    [OSCProperty]
    [Range(-100, 100)]
    public float colorGradingContrast = 0;

    [Header("Bloom")]
    [OSCProperty]
    public float bloomIntensity = 1;
    [OSCProperty]
    public float bloomThreshold = 1;

    public override void Awake()
    {
        if (controller == null)
            controller = GetComponent<PostProcessController>();

        if (controller == null)
        {
            Debug.LogWarning("Can't find a PostProcessController to control !");
            return;
        }

        TargetScript = controller;
        base.Awake();
    }

	public override void OnUiValueChanged(string name) {
		base.OnUiValueChanged(name);
		controller.UpdateProfileValues();
	}
}
