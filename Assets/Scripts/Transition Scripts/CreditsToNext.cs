using UnityEngine;
using System.Collections;

public class CreditsToNext : MonoBehaviour {

	public float transitionTimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transitionTimer -= Time.deltaTime;

		if (transitionTimer <= 0f) {
			Application.LoadLevel (1);
		}
	}
}
