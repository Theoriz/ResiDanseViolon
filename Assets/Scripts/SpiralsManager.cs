using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralsManager : MonoBehaviour
{
    public List<KAPPS.KAPPSSource> vortexes;

    public Vector2 MinMaxInitValue;
    public Vector2 MinMaxRadius;
    public float Lerpstrength;

    public DataAnalyzer analyzer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var vortex in vortexes)
        {
            var sum = Mathf.Abs(analyzer.XDerivated) + Mathf.Abs(analyzer.YDerivated) + Mathf.Abs(analyzer.ZDerivated);
            vortex.radius = Mathf.Lerp(vortex.radius, Remap(sum, MinMaxInitValue.x, MinMaxRadius.x, MinMaxInitValue.y, MinMaxRadius.y), Time.time * Lerpstrength);
        }
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
