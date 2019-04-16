using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLightSensitive : MonoBehaviour
{
    public float intensity;
    public float scale;

    public Lasp.FilterType filterType;

    public Light light;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Lasp.MasterInput.GetPeakLevel(filterType) * scale;
    }
}
