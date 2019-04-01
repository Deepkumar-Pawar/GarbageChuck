using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {


	public bool daytime;

	private Light mainGameLight;
	// Use this for initialization
	void Start () {
		mainGameLight = GameObject.FindGameObjectWithTag ("MainGameLight").GetComponent<Light> ();

		daytime = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (!daytime) {
			mainGameLight.enabled = false;
		} else {
			mainGameLight.enabled = true;
		}
	}
}
