using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAPPSSourceBehaviour : AugmentaBasicPersonBehaviour {

	private KAPPS.KAPPSSource[] sources;

	private float[] sourcesRadius;

	private void Awake() {
		sources = GetComponents<KAPPS.KAPPSSource>();

        sourcesRadius = new float[sources.Length];

        for(int i=0; i<sources.Length; i++)
        {
            sourcesRadius[i] = sources[i].radius;
            sources[i].radius = 0;
        }
    }

	private void Update() {
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].radius = sourcesRadius[i] * AnimatedValue;
        }
	}

}

