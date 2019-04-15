using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAnalyzerControllable : Controllable
{
    [OSCProperty] public float XValue;
    [OSCProperty] public float YValue;
    [OSCProperty] public float ZValue;

    [OSCProperty] public float XDerivated;
    [OSCProperty] public float YDerivated;
    [OSCProperty] public float ZDerivated;
}
