using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau2Controller : MonoBehaviour
{

    public DataAnalyzer analyzer;
    public KAPPS.KAPPS kapps;

    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        kapps._gridStrength = (Mathf.Abs(analyzer.XDerivated) + Mathf.Abs(analyzer.YDerivated) + Mathf.Abs(analyzer.ZDerivated)) * scale;
    }
}
