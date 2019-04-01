using UnityEngine;
using System.Collections;

public class HeadLightModelScript : MonoBehaviour {

	private LightManager lightManager;
	private MeshRenderer myMeshRenderer;
	// Use this for initialization
	void Start () {
		lightManager = GameObject.FindGameObjectWithTag ("LightManager").GetComponent<LightManager> ();
		myMeshRenderer = gameObject.GetComponent<MeshRenderer> ();

		myMeshRenderer.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (lightManager.daytime == false) {
			myMeshRenderer.enabled = true;
		} else {
			myMeshRenderer.enabled = false;
		}
	}
}
