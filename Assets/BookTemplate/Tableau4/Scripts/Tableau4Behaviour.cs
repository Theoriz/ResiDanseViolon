using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau4Behaviour : AugmentaBasicPersonBehaviour {

	private KAPPS.KAPPSSource noiseDeformerSource;

	private float noiseDeformerRadius;

	private void Awake() {
		noiseDeformerSource = GetComponent<KAPPS.KAPPSSource>();

		noiseDeformerRadius = noiseDeformerSource.radius;
        noiseDeformerSource.radius = 0;
    }

	private void Update() {
		noiseDeformerSource.radius = noiseDeformerRadius * AnimatedValue;
	}

}

