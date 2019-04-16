using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau4LightsController : MonoBehaviour
{
	public KAPPS.KAPPS kapps;
	public GameObject lightPrefab;
	public DataAnalyzer analyzer;

	public bool audioReactive;
	public float threshold;

	public float floor;

	public Color color;

	public float minRadius;
	public float maxRadius;

	public float minIntensity;
	public float maxIntensity;

	public float minFrequency;
	public float maxFrequency;

	public float minSpeed;
	public float maxSpeed;

	private float timer;
	private float currentFrequency;


	private void Start() {
		timer = 0;
		currentFrequency = Random.Range(minFrequency, maxFrequency);
	}

	// Update is called once per frame
	void Update() {

		if (audioReactive) {

			if ((Mathf.Abs(analyzer.XDerivated) + Mathf.Abs(analyzer.YDerivated) + Mathf.Abs(analyzer.ZDerivated)) > threshold)
				CreateLight();

		} else {
			timer += Time.deltaTime;

			if (timer > 1.0f / currentFrequency) {

				CreateLight();
				timer = 0;
				currentFrequency = Random.Range(minFrequency, maxFrequency);

			}
		}
	}

	void CreateLight() {

		GameObject newLight = Instantiate(lightPrefab, transform);

		Tableau4Light lightParam = newLight.GetComponent<Tableau4Light>();

		lightParam.kapps = kapps;
		lightParam.speed = Random.Range(minSpeed, maxSpeed);
		lightParam.floor = floor;
		lightParam.radius = Random.Range(minRadius, maxRadius);
		lightParam.intensity = Random.Range(minIntensity, maxIntensity);
		lightParam.color = color;
	}
}
