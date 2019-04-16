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
            vortex.radius = Mathf.Lerp(vortex.radius, Remap(sum, MinMaxInitValue.x, MinMaxInitValue.y, MinMaxRadius.x, MinMaxRadius.y), Time.time * Lerpstrength);
        }
    }

    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}
