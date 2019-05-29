using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentaMrScene : MrScene {

    protected bool _isFading = false;
    protected float _fadeValue = 0.0f;

    public AugmentaBookBasicManager augmentaBookBasicManager;

    public AnimationCurve FadeInCurve;
    public AnimationCurve FadeOutCurve;

    public Color FadingColor;

    private CameraFadeToColor[] FadeScripts;

    public virtual void Awake()
    {
        FadeScripts = GameObject.FindObjectsOfType<CameraFadeToColor>();

        for (int i = 0; i < FadeScripts.Length; i++)
        {
            FadeScripts[i].ChangeFadeColor(FadingColor);
        }


        if (!augmentaBookBasicManager)
            augmentaBookBasicManager = GameObject.FindObjectOfType<AugmentaBookBasicManager>();

        _fadeValue = 0.0f;

        //SetSceneVolume(_fadeValue * augmentaBookBasicManager.sceneVolume);
    }

    public virtual void Update()
    {
        if (_isFading)
        {
            for (int i = 0; i < FadeScripts.Length; i++)
            {
                FadeScripts[i].FadeValue = 1.0f - _fadeValue;
            }
        }
    }

    public override IEnumerator Intro()
    {
        float currentTime = 0.0f;
        _isFading = true;

        while (currentTime < introDuration)
        {
            currentTime += Time.deltaTime;
            _fadeValue = FadeInCurve.Evaluate(Mathf.Clamp01(currentTime / introDuration));
            //SetSceneVolume(_fadeValue * augmentaBookBasicManager.sceneVolume);
            yield return new WaitForFixedUpdate();
        }

        _isFading = false;
    }

    public override IEnumerator Outro()
    {
        float currentTime = 0.0f;
        _isFading = true;

        while (currentTime < outroDuration)
        {
            currentTime += Time.deltaTime;
            _fadeValue = FadeOutCurve.Evaluate(Mathf.Clamp01(currentTime / outroDuration));
            //SetSceneVolume(_fadeValue * augmentaBookBasicManager.sceneVolume);
            yield return new WaitForFixedUpdate();
        }

        _isFading = false;
    }
}
