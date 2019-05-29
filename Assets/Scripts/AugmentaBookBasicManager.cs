using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentaBookBasicManager : AugmentaAreaAnchor {

    [Range(0, 1)]
    private float _sceneVolume = 1;
    public float sceneVolume
    {
        get
        {
            return _sceneVolume;
        }
        set
        {
            _sceneVolume = Mathf.Clamp01(value);
            if (SoundManager.instance)
            {
                SoundManager.instance.masterVolume = _sceneVolume;
            }
            else
            {
                AudioListener.volume = _sceneVolume;
            }
        }
    }
}
