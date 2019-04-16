using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau4Light : MonoBehaviour
{
	public KAPPS.KAPPS kapps;
	public float floor;
	public float speed;
	public float radius;
	public float intensity;
	public Color color;

	private float age = 0;

	// Start is called before the first frame update
	void Start() {
		age = 0;

		Light light = GetComponent<Light>();
		light.intensity = intensity;
		light.range = radius;
		light.color = color;

		KAPPS.KAPPSSource source = GetComponent<KAPPS.KAPPSSource>();
		source.particleSystemList = new List<KAPPS.KAPPS>();
		source.particleSystemList.Add(kapps);

		source.radius = radius;
		source.intensity = intensity;

		//Enable the source that is disabled in the prefab to not initialize itself when instantiated
		source.enabled = true;
	}

	// Update is called once per frame
	void Update() {

		age += Time.deltaTime;

		transform.Translate(0, -Time.deltaTime * speed, 0);

		if (transform.localPosition.y < floor) {
			Destroy(gameObject);
		}
	}
}
