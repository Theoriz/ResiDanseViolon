using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactiveKapps : MonoBehaviour
{
    public Vector2 minMaxNoiseScale;

    public Lasp.FilterType filterType;
    public float scale;
    public float lerpStrength;
    public KAPPS.KAPPS kapps;

    // Update is called once per frame
    void Update()
    {
        var value = Lasp.MasterInput.GetPeakLevel(filterType) * scale;
        kapps._noiseFieldPositionScale = Mathf.Lerp(kapps._noiseFieldPositionScale, Mathf.Clamp(value, minMaxNoiseScale.x, minMaxNoiseScale.y), lerpStrength);
    }
}
