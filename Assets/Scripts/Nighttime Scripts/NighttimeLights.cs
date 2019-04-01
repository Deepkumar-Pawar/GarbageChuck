using UnityEngine;
using System.Collections;

public class NighttimeLights : MonoBehaviour {

	public float lightIntensity;

	private LightManager lightManager;
	private Light myLight;
	// Use this for initialization
	void Start () {
		lightManager = GameObject.FindGameObjectWithTag ("LightManager").GetComponent<LightManager> ();
		myLight = gameObject.GetComponent<Light> ();

		myLight.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (lightManager.daytime == false) {
			myLight.enabled = true;
		} else {
			myLight.enabled = false;
		}
		myLight.intensity = lightIntensity;
	}
}