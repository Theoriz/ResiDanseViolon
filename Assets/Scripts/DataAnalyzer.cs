using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAnalyzer : MonoBehaviour
{
    [Header("Hot hand")]
    public float XValue;
    public float YValue;
    public float ZValue;

    public float XDerivated;
    public float YDerivated;
    public float ZDerivated;

    private float _xOldValue;
    private float _yOldValue;
    private float _zOldValue;

    public float refreshRate;
    private float _lastTimeRefreshed;

    // Start is called before the first frame update
    void Start()
    {
        _xOldValue = XValue;
        _yOldValue = YValue;
        _zOldValue = ZValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _lastTimeRefreshed >= (1/refreshRate))
        {
            XDerivated = XValue - _xOldValue;
            _xOldValue = XValue;

            YDerivated = YValue - _yOldValue;
            _yOldValue = YValue;

            ZDerivated = ZValue - _zOldValue;
            _zOldValue = ZValue;

            _lastTimeRefreshed = Time.time;
        }
    }
}
