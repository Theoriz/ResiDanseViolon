using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    private bool _muted;
    public bool muted
    {
        get
        {
            return _muted;
        }
        set
        {
            _muted = value;
            if (_muted)
            {
                MuteSound();
            } else
            {
                UnmuteSound();
            }
        }
    }

    [Range(0, 1)]
    private float _masterVolume = 1;
    public float masterVolume
    {
        get
        {
            return _masterVolume;
        }
        set
        {
            if (!muted)
            {
                _masterVolume = Mathf.Clamp01(value);
                SetMasterVolume(_masterVolume);
            }    
        }
    }

    private float oldSoundValue;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
    void MuteSound()
    {
        oldSoundValue = masterVolume;
        SetMasterVolume(0);
    }

    void UnmuteSound()
    {
        masterVolume = oldSoundValue;
    }

    private void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
